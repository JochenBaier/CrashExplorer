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
  public class TestNotepadPlusPlusAccessViolation
  {
    [TestMethod]
    public void TestAccessViolation()
    {
      var mapFile = @"..\..\TestFiles\notepad++.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x000000000020181bul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x000000014020181bul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x000000000000758bul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x00000001401fa290ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?command@Notepad_plus@@AEAAXH@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("NppCommands.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(5602, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\NppCommands.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x000000000000758bul, cod_result.AddressInFunction);
      Assert.AreEqual(0x0001cfea, cod_result.CodFileLineNumber);
      Assert.AreEqual(@"..\..\TestFiles\NppCommands.cod", cod_result.CodFullPathName);
      Assert.AreEqual("Notepad_plus::command", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(0x00000a4f, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\notepadplusplus\\notepad-plus-plus-master\\PowerEditor\\src\\NppCommands.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0x00000000, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);
      
      string expectedCodeBlock = @"2639 : 
2640 :       int* tmp_buffer = nullptr;
2641 :       *tmp_buffer = 42; //TEST crash Notepad++: access violation";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"0758b  c7 03 2a 00 00
00     mov   DWORD PTR [rbx], 42  ; 0000002aH";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);

    }
  }
}
