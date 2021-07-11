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

using CrashExplorer.library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
  [TestClass]
  public class TestExceptionCodes
  {
    [TestMethod]
    public void TestToString()
    {
      Assert.AreEqual("ACCESS_VIOLATION", ExceptionCodes.ToString(0xc0000005));
      Assert.AreEqual("INTEGER_DIVIDE_BY_ZERO", ExceptionCodes.ToString(0xc0000094));
      Assert.AreEqual("STACK_OVERFLOW", ExceptionCodes.ToString(0xc00000fd));
    }
  }
}
