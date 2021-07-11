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
using System.IO;

namespace CrashExplorer.library
{
  /// <summary>
  /// Helper related to file access
  /// </summary>
  ///
  public static class FileSystemHelper
  {
    public static string[] ReadFile(FunctionResult functionResult, string fileName)
    {
      try
      {
        return File.ReadAllLines(fileName);
      }
      catch (Exception e)
      {
        functionResult.SetError($"Failed to read file '{fileName}'.\n{e.Message}");
        return null;
      }
    }

    public static string FindCodFileInFolder(FunctionResult functionResult, string basePath, string fileName)
    {
      string[] files = Directory.GetFiles(basePath, fileName, SearchOption.AllDirectories);

      if (files.Length == 0)
      {
        functionResult.SetError($"No cod file with name '{fileName}' in folder '{basePath}' (including sub folders) found");
        return null;
      }

      if (files.Length == 1)
      {
        return files[0];
      }

      functionResult.SetError($"Multible cod files with name '{fileName}' found:\n{string.Join("\n", files)}\n\nPlease limit listing files search path");
      return null;
    }
  }
}
