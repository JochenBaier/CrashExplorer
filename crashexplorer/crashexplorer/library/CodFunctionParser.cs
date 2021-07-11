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

using System.Collections.Generic;
using System.Diagnostics;

namespace CrashExplorer.library
{
  /// <summary>
  /// Helper to parse functions within listing files (*.cod)
  /// </summary>
  ///
  public static class CodFunctionParser
  {
    public static void ParseFunction(FunctionResult functionResult, string functionNameUndecorated, int functionStartIndex, int functionEndIndex, string[] lines, string addressString, out int lineInCodFile, out int lineInSourceFile, List<string> sourceCodeBlock, List<string> assemblyCodeBlock, out int assemblyCodeBlockMark)
    {
      assemblyCodeBlockMark = 0;
      lineInCodFile = 0;
      lineInSourceFile = 0;
      List<int> last_empty_lines = new List<int>();

      //search address
      int address_index = -1;
      for (int i = functionStartIndex + 1; i < functionEndIndex; ++i)
      {
        string line = lines[i];

        if (line.Length == 0)
        {
          last_empty_lines.Add(i);
        }

        if (line.StartsWith(addressString))
        {
          address_index = i;
          break;
        }
      }

      if (address_index == -1)
      {
        functionResult.SetError($"Address '{addressString.TrimStart()}' within function '{functionNameUndecorated}' not found.");
        return;
      }

      //Find next empty line
      for (int i = address_index + 1; i < functionEndIndex; ++i)
      {
        string line = lines[i];

        bool is_assembly_block = line.Length > 0 && (line.StartsWith("  ") || line.StartsWith("\t"));
        if (is_assembly_block)
        {
          continue;
        }

        last_empty_lines.Add(i);
        break;
      }

      Debug.Assert(last_empty_lines.Count >= 3);

      int source_block_start_index = last_empty_lines[last_empty_lines.Count - 3] + 1;
      int source_block_end_index = last_empty_lines[last_empty_lines.Count - 2] - 1;

      int assembly_block_start_index = last_empty_lines[last_empty_lines.Count - 2] + 1;
      int assembly_block_end_index = last_empty_lines[last_empty_lines.Count - 1] - 1;
      assemblyCodeBlockMark = address_index - assembly_block_start_index;

      //source code block
      for (int i = source_block_start_index; i <= source_block_end_index; ++i)
      {
        Debug.Assert(lines[i].Length > 2);
        sourceCodeBlock.Add(lines[i].Substring(2).Replace("\t", "  "));
      }

      lineInSourceFile = ParseSourceCodeLineNumer(lines[source_block_start_index]);
      lineInCodFile = source_block_start_index + 1;

      //assembly code block
      for (int i = assembly_block_start_index; i <= assembly_block_end_index; ++i)
      {
        string assembly_line = lines[i];

        if (assembly_line.StartsWith("\t"))
        {
          assemblyCodeBlock.Add(assembly_line.Substring(1).Replace("\t", "  "));
        }
        else
        {
          if (assembly_line.StartsWith("  "))
          {
            Debug.Assert(assembly_line.Length > 2);
            assembly_line = assembly_line.Substring(2);
          }

          assemblyCodeBlock.Add(assembly_line.Replace("\t", "  "));
        }
      }
    }

    private static int ParseSourceCodeLineNumer(string sourceCodeLine)
    {
      Debug.Assert(sourceCodeLine.Length > 2);

      int index_of_double_point = sourceCodeLine.IndexOf(':');
      Debug.Assert(index_of_double_point != -1);

      string lineNumberString = sourceCodeLine.Substring(2, index_of_double_point - 2).TrimEnd();

      bool ok = int.TryParse(lineNumberString, out int lineNumber);
      Debug.Assert(ok);

      return lineNumber;
    }
  }
}