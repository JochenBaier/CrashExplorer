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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CrashExplorer.library;

namespace UnitTest
{
  //Note: Notepad++ is used here for a real test.The errors are intentionally included and are not in the orignal Notepad++ code.

  [TestClass]
  public class TestNotepadPlusPlusDivideByZero
  {
    [TestMethod]
    public void TestDivideByZero()
    {
      var mapFile = @"..\..\TestFiles\notepad++.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000261bc9ul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000140261bc9ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000129ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000140261aa0ul, map_file_results.FileFunction.Address);
      Assert.AreEqual( "?removeAnyDuplicateLines@ScintillaEditView@@QEAAXXZ", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual( "ScintillaEditView.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(6199, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\ScintillaEditView.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x0000000000000129ul, cod_result.AddressInFunction);
      Assert.AreEqual(68749, cod_result.CodFileLineNumber);
      Assert.AreEqual(@"..\..\TestFiles\ScintillaEditView.cod", cod_result.CodFullPathName);
      Assert.AreEqual( "ScintillaEditView::removeAnyDuplicateLines", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(3927, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\notepadplusplus\\notepad-plus-plus-master\\PowerEditor\\src\\ScintillaComponent\\ScintillaEditView.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0x00000002, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);
      
      string expectedCodeBlock= @"3927 :   {
3928 :     return;
3929 :   }
3930 : 
3931 :   for (int i = 0; i < 10; i++)
3932 :   {
3933 :     toLine = i / fromLine; //TEST crash Notepad++: divide by zero";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"00122  33 d2     xor   edx, edx
00124  b8 09 00 00 00   mov   eax, 9
00129  48 f7 f7   div   rdi
0012c  4c 8b f0   mov   r14, rax";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);

    }
  }
}
