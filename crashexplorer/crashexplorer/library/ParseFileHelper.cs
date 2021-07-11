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
using System.Diagnostics;

namespace CrashExplorer.library
{
  /// <summary>
  /// Helper related to file parsing
  /// </summary>
  ///
  public static class ParseFileHelper
  {
    public static bool SkipLinesUntilItContains(string text, string[] lines, ref int lineIndex)
    {
      for (; lineIndex < lines.Length; ++lineIndex)
      {
        string line = lines[lineIndex];
        int index = line.IndexOf(text, StringComparison.Ordinal);
        if (index != -1)
        {
          return true;
        }
      }

      return false;
    }
    public static string GetUndecoratedFunctionName(string decoratedFunctionName)
    {
      if (!decoratedFunctionName.StartsWith("?"))
      {
        return decoratedFunctionName;
      }

      Debug.Assert(!decoratedFunctionName.StartsWith("??"));

      string[] splitted = decoratedFunctionName.Substring(1).Split('@');
      Debug.Assert(splitted.Length >= 3);

      if (string.IsNullOrEmpty(splitted[1]))
      {
        return splitted[0];
      }

      string undecoratedFunctionName = $"{splitted[1]}::{splitted[0]}";
      return undecoratedFunctionName;
    }
  }
}