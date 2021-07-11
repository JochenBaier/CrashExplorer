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
using System.Globalization;

namespace CrashExplorer.library
{
  public class MapFileResults
  {
    public string MapFilePath { get; set; }
    public MapFileFunction FileFunction { get; set; }
    public ulong PreferredLoadAddress { get; set; }
    public ulong AddressToSearch { get; set; }

    public ulong AddressWithinFunction { get; set; }
  }

  public class MapFileFunction
  {
    public string FunctionName { get; set; }
    public ulong Address { get; set; }
    public string LibraryName { get; set; }
    public string ObjectName { get; set; }
    public int MapFileLineNumber { get; set; }
  }

  /// <summary>
  /// Helper to parse map files (*.map)
  /// </summary>
  ///
  public static class MapFileParser
  {
    public static MapFileResults ParseMapFileAsync(FunctionResult functionResult, string mapFilePath, ulong crashOffset)
    {
      string[] lines = FileSystemHelper.ReadFile(functionResult, mapFilePath);
      if (functionResult.IsBad)
      {
        return null;
      }

      int line_index = 0;

      ulong preferred_load_address = ParsePreferredLoadAddress(functionResult, lines, ref line_index);
      if (functionResult.IsBad)
      {
        return null;
      }

      bool found = ParseFileHelper.SkipLinesUntilItContains("Rva+Base", lines, ref line_index);
      if (!found)
      {
        functionResult.SetError("Not a valid map file. Line with string 'Rva+Base' not found.");
        return null;
      }

      ulong address_to_search = preferred_load_address + crashOffset;
      MapFileFunction matchingFunction = FindMatchingFunction(functionResult, lines, ref line_index, address_to_search);
      if (functionResult.IsBad)
      {
        return null;
      }

      MapFileResults map_file_results = new MapFileResults
      {
        MapFilePath = mapFilePath,
        FileFunction = matchingFunction,
        AddressWithinFunction = address_to_search - matchingFunction.Address,
        PreferredLoadAddress = preferred_load_address,
        AddressToSearch = address_to_search
      };

      return map_file_results;
    }

    private static ulong ParsePreferredLoadAddress(FunctionResult functionResult, string[] lines, ref int lineIndex)
    {
      const string preferred_load_address_text = " Preferred load address is ";

      bool found = ParseFileHelper.SkipLinesUntilItContains(preferred_load_address_text, lines, ref lineIndex);
      if (!found)
      {
        functionResult.SetError($"Not a valid map file. String '{preferred_load_address_text}' not found.");
        return 0;
      }

      string load_address_as_text = lines[lineIndex].Substring(preferred_load_address_text.Length);
      bool ok = ulong.TryParse(load_address_as_text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out ulong preferred_load_address);
      if (!ok)
      {
        functionResult.SetError($"Not a valid map file. Preferred load address is value '{load_address_as_text}' not a valid hex number.");
        return 0;
      }

      return preferred_load_address;
    }

    private static MapFileFunction FindMatchingFunction(FunctionResult functionResult, IReadOnlyList<string> lines, ref int lineIndex, ulong addressToSearch)
    {
      List<MapFileFunction> matching_functions = new List<MapFileFunction>();

      for (; lineIndex < lines.Count - 1; ++lineIndex)
      {
        string line = lines[lineIndex];

        //find a function line
        string[] address_line_parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (address_line_parts.Length != 5 || address_line_parts[3] != "f")
        {
          continue;
        }

        Debug.Assert(address_line_parts[0].StartsWith("0001:"));

        ExtractFunctionAddress(functionResult, lineIndex, line, address_line_parts, out var function_address);
        if (functionResult.IsBad)
        {
          return null;
        }

        if (addressToSearch < function_address)
        {
          continue;
        }

        MapFileFunction map_file_function = ParseMapFileFunction(functionResult, lines, lineIndex, addressToSearch, function_address, address_line_parts);
        if (functionResult.IsBad)
        {
          return null;
        }

        if (map_file_function != null)
        {
          matching_functions.Add(map_file_function);
        }
      }

      if (matching_functions.Count == 0)
      {
        functionResult.SetError(
          $"No function matching the address '0x{addressToSearch:x}' found. Please check if the map file is of the faulting module.");
        return null;
      }

      MapFileFunction mapFileFunction = ReturnFunctionWithClosestOffset(matching_functions, addressToSearch);
      return mapFileFunction;
    }

    private static MapFileFunction ReturnFunctionWithClosestOffset(IReadOnlyList<MapFileFunction> mapFileFunctions, ulong offsetToFind)
    {
      int currentIndex = -1;
      ulong currentOffsetDistance = ulong.MaxValue;

      for (var i = 0; i < mapFileFunctions.Count; i++)
      {
        var mapFileFunction = mapFileFunctions[i];

        Debug.Assert(offsetToFind >= mapFileFunction.Address);
        ulong offsetDistance = offsetToFind - mapFileFunction.Address;

        if (offsetDistance > currentOffsetDistance)
        {
          continue;
        }

        currentOffsetDistance = offsetDistance;
        currentIndex = i;
      }

      return mapFileFunctions[currentIndex];
    }

    private static MapFileFunction ParseMapFileFunction(FunctionResult functionResult, IReadOnlyList<string> lines, int lineIndex,
      ulong addressToSearch, ulong functionAddress, string[] addressLineParts)
    {
      //check address in next line
      int nextLineIndex = lineIndex + 1;
      Debug.Assert(nextLineIndex < lines.Count);

      string nextLine = lines[nextLineIndex];

      string[] next_address_line_parts = nextLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      ExtractFunctionAddress(functionResult, nextLineIndex, nextLine, next_address_line_parts, out ulong function_address_next_line);
      if (functionResult.IsBad)
      {
        return null;
      }

      if (addressToSearch >= function_address_next_line)
      {
        return null;
      }

      Debug.Assert(addressToSearch >= functionAddress && functionAddress < addressToSearch);

      //Found matching function
      MapFileFunction map_file_function = new MapFileFunction
      {
        FunctionName = addressLineParts[1],
        Address = functionAddress,
        MapFileLineNumber = lineIndex + 1
      };

      string[] lib_and_object_name = addressLineParts[4].Split(':');
      Debug.Assert(lib_and_object_name.Length == 1 || lib_and_object_name.Length == 2);
      if (lib_and_object_name.Length == 1)
      {
        map_file_function.ObjectName = lib_and_object_name[0];
      }
      else if (lib_and_object_name.Length == 2)
      {
        map_file_function.LibraryName = lib_and_object_name[0];
        map_file_function.ObjectName = lib_and_object_name[1];
      }

      return map_file_function;
    }

    private static void ExtractFunctionAddress(FunctionResult functionResult, int lineIndex, string line, string[] addressLineParts,
      out ulong functionAddress)
    {
      functionAddress = 0;

      if (addressLineParts.Length < 4)
      {
        functionResult.SetError(
          $"Not a valid map file. Line '{line}' at line nr '{lineIndex + 1}' not a valid");
        return;
      }

      bool ok = ulong.TryParse(addressLineParts[2], NumberStyles.HexNumber, CultureInfo.InvariantCulture,
        out functionAddress);
      if (!ok)
      {
        functionResult.SetError(
          $"Not a valid map file. Text '{addressLineParts[2]}' in line '{lineIndex + 1}' not a valid hex number");
      }

    }
  }
}