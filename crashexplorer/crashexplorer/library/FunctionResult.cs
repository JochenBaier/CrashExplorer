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

namespace CrashExplorer.library
{
  /// <summary>
  /// Error handling for method/function calls
  /// </summary>
  ///
  public class FunctionResult
  {
    public void SetError(string errorText)
    {
      ErrorText = errorText;
      IsBad = true;
    }

    public string ErrorText { get; private set; }
    public bool IsBad { get; private set; }
  }
}
