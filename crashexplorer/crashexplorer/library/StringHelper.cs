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

using System.Globalization;

namespace CrashExplorer.library
{
  /// <summary>
  /// Helper related to strings
  /// </summary>
  ///
  public static class StringHelper
  {
    public static bool ToHexNumber(string hexText, out ulong hexValue)
    {
      hexValue = 0;
      if (hexText.ToLower().StartsWith("0x"))
      {
        hexText = hexText.Substring(2);
      }

      if (string.IsNullOrEmpty(hexText))
      {
        return false;
      }

      bool ok = ulong.TryParse(hexText, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out hexValue);
      return ok;
    }
    public static string RemoveQuotes(string text)
    {
      if (text.StartsWith("\"") && text.EndsWith("\""))
      {
        return text.Substring(1, text.Length - 2);
      }

      return text;
    }
  }
}
