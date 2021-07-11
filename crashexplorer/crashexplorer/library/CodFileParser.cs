/*
   This file is part of CrashExplorer.
   
   CrashExplorer is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.
   
   CrashExplorer is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with CrashExplorer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CrashExplorer.library
{
  public class CodResult
  {
    public int CodFileLineNumber { get; set; }
    public int SourceFileLineNumber { get; set; }
    public string CodFullPathName { get; set; }
    public ulong AddressInFunction { get; set; }
    public string FunctionNameUndecorated { get; set; }
    public List<string> SourceCodeBlock { get; set; }
    public string SourceFileName { get; set; }
    public List<string> AssemblyCodeBlock { get; set; }
    public int AssemblyBlockMark { get; set; }
  }

  /// <summary>
  /// Helper to parse listing files (*.cod)
  /// </summary>
  ///
  public static class CodFileParser
  {
    public static CodResult ParseCodFile(FunctionResult functionResult, string codFileFullPath, MapFileResults mapFileResults)
    {
      string[] lines = FileSystemHelper.ReadFile(functionResult, codFileFullPath);
      if (functionResult.IsBad)
      {
        return null;
      }

      CheckFileHeader(functionResult, lines);
      if (functionResult.IsBad)
      {
        return null;
      }

      SearchFunctionBlock(functionResult, mapFileResults, lines, out int function_start_index, out int function_end_index, out string function_name_undecorated, out string source_filename);
      if (functionResult.IsBad)
      {
        return null;
      }

      string address_string = $"  {mapFileResults.AddressWithinFunction:x5}";
      List<string> p_source_code_block = new List<string>();
      List<string> p_assembly_code_block = new List<string>();
      CodFunctionParser.ParseFunction(functionResult, function_name_undecorated, function_start_index, function_end_index, lines, address_string, out int p_line_in_cod_file,
        out int p_line_in_source_file, p_source_code_block, p_assembly_code_block, out int assembly_code_block);
      if (functionResult.IsBad)
      {
        return null;
      }

      CodResult cod_result = new CodResult
      {
        CodFullPathName = codFileFullPath,
        FunctionNameUndecorated = function_name_undecorated,
        SourceCodeBlock = p_source_code_block,
        AssemblyCodeBlock = p_assembly_code_block,
        AssemblyBlockMark = assembly_code_block,
        CodFileLineNumber = p_line_in_cod_file,
        SourceFileLineNumber = p_line_in_source_file,
        AddressInFunction = mapFileResults.AddressWithinFunction,
        SourceFileName = source_filename
      };

      return cod_result;
    }
    private static void SearchFunctionBlock(FunctionResult functionResult, MapFileResults mapFileResults, string[] lines,
      out int functionStartIndex, out int functionEndIndex,
      out string functionNameUndecorated, out string sourceFilename)
    {
      functionNameUndecorated = "---";
      functionStartIndex = -1;
      functionEndIndex = -1;
      sourceFilename = "---";

      int last_source_filename_index = 0;
      int line_index = 10;

      //search line with function name + PROC
      line_index = FindFunctionStart(mapFileResults, lines, ref functionStartIndex, ref line_index, ref last_source_filename_index);
      if (line_index == lines.Length)
      {
        functionResult.SetError($"Function '{mapFileResults.FileFunction.FunctionName}' not found in the cod file");
        return;
      }

      string function_line = lines[line_index].Replace("\t", " ");
      ParseFunctionLine(mapFileResults, out functionNameUndecorated, function_line);

      string function_name_plus_endp = mapFileResults.FileFunction.FunctionName + " ENDP";

      //Function End
      for (; line_index < lines.Length; ++line_index)
      {
        string line = lines[line_index].Replace("\t", " ");
        if (line.StartsWith(function_name_plus_endp))
        {
          break;
        }
      }

      if (line_index == lines.Length)
      {
        functionResult.SetError($"Function end string '{function_name_plus_endp}' not found in cod file. Please report.");
        return;
      }

      functionEndIndex = line_index;
      sourceFilename = lines[last_source_filename_index].Substring(7);
    }

    private static int FindFunctionStart(MapFileResults mapFileResults, string[] lines, ref int functionStartIndex,
      ref int lineIndex, ref int lastSourceFilenameIndex)
    {
      for (; lineIndex < lines.Length; ++lineIndex)
      {
        string line = lines[lineIndex].Replace("\t", " ");

        if (line.StartsWith("; File "))
        {
          lastSourceFilenameIndex = lineIndex;
        }

        if (!line.StartsWith(mapFileResults.FileFunction.FunctionName))
        {
          continue;
        }

        string[] line_splitted = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (line_splitted.Length < 3 || line_splitted[0] != mapFileResults.FileFunction.FunctionName || line_splitted[1] != "PROC")
        {
          continue;
        }

        functionStartIndex = lineIndex;
        break;
      }

      return lineIndex;
    }

    private static void ParseFunctionLine(MapFileResults mapFileResults, out string functionNameUndecorated,
      string functionLine)
    {
      if (functionLine.StartsWith("?"))
      {
        const string comdatText = ", COMDAT";
        if (functionLine.EndsWith(comdatText))
        {
          functionLine = functionLine.Substring(0, functionLine.Length - comdatText.Length);
        }

        int index_of_semicolen = functionLine.LastIndexOf(";");
        Debug.Assert(index_of_semicolen != -1);

        functionNameUndecorated = functionLine.Substring(index_of_semicolen + 2);
      }
      else
      {
        functionNameUndecorated = mapFileResults.FileFunction.FunctionName;
      }
    }

    private static void CheckFileHeader(FunctionResult functionResult, string[] lines)
    {
      if (lines.Length < 10)
      {
        functionResult.SetError("Not a valid cod file. Not enough lines.");
        return;
      }

      const string cod_header = "; Listing generated by Microsoft (R)";
      if (!lines[0].StartsWith(cod_header))
      {
        functionResult.SetError($"Not a valid cod file. Header '{cod_header}' missing");
      }
    }
  }
}