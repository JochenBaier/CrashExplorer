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
  public class TestNotepadPlusPlusCallstack
  {
    [TestMethod]
    public void TestNotepad__Notepad_plus_Window_Notepad_plus_Proc_0x51()
    {
      var mapFile = @"..\..\TestFiles\notepad++.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();

      ulong moduleBaseAddress = 0x9A3F0000ul;

      ulong offsetToFind = 0x9a5d6131ul - moduleBaseAddress;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);
 
      Assert.AreEqual(0x00000001401e6131ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000051ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual( 0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x00000001401e60e0ul, map_file_results.FileFunction.Address );
      Assert.AreEqual("?Notepad_plus_Proc@Notepad_plus_Window@@CA_JPEAUHWND__@@I_K_J@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("NppBigSwitch.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(5492, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\NppBigSwitch.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x0000000000000051ul, cod_result.AddressInFunction);
      Assert.AreEqual(20393, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\NppBigSwitch.cod", cod_result.CodFullPathName);
      Assert.AreEqual("Notepad_plus_Window::Notepad_plus_Proc", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(79, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\notepadplusplus\\notepad-plus-plus-master\\PowerEditor\\src\\NppBigSwitch.cpp", cod_result.SourceFileName);
      Assert.AreEqual(8, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);
      
      string expectedCodeBlock= @"79   :     }
80   : 
81   :     default:
82   :     {
83   :       return (reinterpret_cast<Notepad_plus_Window *>(::GetWindowLongPtr(hwnd, GWLP_USERDATA))->runProc(hwnd, message, wParam, lParam));";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"00035  ff 15 00 00 00
00     call   QWORD PTR __imp_GetWindowLongPtrW
0003b  4c 8b cd   mov   r9, rbp
0003e  48 89 74 24 20   mov   QWORD PTR [rsp+32], rsi
00043  48 8b c8   mov   rcx, rax
00046  44 8b c7   mov   r8d, edi
00049  48 8b d3   mov   rdx, rbx
0004c  e8 00 00 00 00   call   ?runProc@Notepad_plus_Window@@AEAA_JPEAUHWND__@@I_K_J@Z ; Notepad_plus_Window::runProc
00051  eb 35     jmp   SHORT $LN1@Notepad_pl";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }

    [TestMethod]
    public void Testnotepad__Notepad_plus_Window_runProc_0x42()
    {
      var mapFile = @"..\..\TestFiles\notepad++.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();

      ulong moduleBaseAddress = 0x9A3F0000ul;

      ulong offsetToFind = 0x9a5db6a2ul - moduleBaseAddress;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x00000001401eb6a2ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000042ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x00000001401eb660ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?runProc@Notepad_plus_Window@@AEAA_JPEAUHWND__@@I_K_J@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("NppBigSwitch.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(5501, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\NppBigSwitch.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x0000000000000042ul, cod_result.AddressInFunction);
      Assert.AreEqual(20154, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\NppBigSwitch.cod", cod_result.CodFullPathName);
      Assert.AreEqual("Notepad_plus_Window::runProc", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(127, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\notepadplusplus\\notepad-plus-plus-master\\PowerEditor\\src\\NppBigSwitch.cpp", cod_result.SourceFileName);
      Assert.AreEqual(5, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"127  :         return _notepad_plus_plus_core.process(hwnd, message, wParam, lParam);";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"0002c  48 83 c1 20   add   rcx, 32      ; 00000020H
00030  48 8b 84 24 b0
00 00 00   mov   rax, QWORD PTR lParam$[rsp]
00038  48 89 44 24 20   mov   QWORD PTR [rsp+32], rax
0003d  e8 00 00 00 00   call   ?process@Notepad_plus@@QEAA_JPEAUHWND__@@I_K_J@Z ; Notepad_plus::process
00042  90     npad   1";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }

    [TestMethod]
    public void Testnotepad__notepad__Notepad_plus__process_0xd9a()
    {
      var mapFile = @"..\..\TestFiles\notepad++.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();

      ulong moduleBaseAddress = 0x9A3F0000ul;

      ulong offsetToFind = 0x9a5d70daul - moduleBaseAddress;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x00000001401e70daul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000d9aul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x00000001401e6340ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?process@Notepad_plus@@QEAA_JPEAUHWND__@@I_K_J@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("NppBigSwitch.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(5499, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\NppBigSwitch.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x0000000000000d9aul, cod_result.AddressInFunction);
      Assert.AreEqual(23015, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\NppBigSwitch.cod", cod_result.CodFullPathName);
      Assert.AreEqual("Notepad_plus::process", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(710, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\notepadplusplus\\notepad-plus-plus-master\\PowerEditor\\src\\NppBigSwitch.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"710  :       }
711  :       return TRUE;";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"00d9a  49 8b c4   mov   rax, r12
00d9d  e9 67 40 00 00   jmp   $LN1@process";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }

    [TestMethod]
    public void Testnotepad__notepad__Notepad_plus__command_0x2bd3()
    {
      var mapFile = @"..\..\TestFiles\notepad++.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();

      ulong moduleBaseAddress = 0x9A3F0000ul;

      ulong offsetToFind = 0x9a5ece63ul - moduleBaseAddress;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x00000001401fce63ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000002bd3ul, map_file_results.AddressWithinFunction);
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


      Assert.AreEqual(0x0000000000002bd3ul, cod_result.AddressInFunction);
      Assert.AreEqual(107676, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\NppCommands.cod", cod_result.CodFullPathName);
      Assert.AreEqual("Notepad_plus::command", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(1617, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\notepadplusplus\\notepad-plus-plus-master\\PowerEditor\\src\\NppCommands.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"1617 :       _pEditView->execute(SCI_ENDUNDOACTION);";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"02bd3  45 33 c9   xor   r9d, r9d
02bd6  45 33 c0   xor   r8d, r8d
02bd9  ba 1f 08 00 00   mov   edx, 2079    ; 0000081fH";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }

    [TestMethod]
    public void Testnotepad__ScintillaEditView__removeAnyDuplicateLines_0x129()
    {
      var mapFile = @"..\..\TestFiles\notepad++.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();

      ulong moduleBaseAddress = 0x9A3F0000ul;

      ulong offsetToFind = 0x9a651bc9ul - moduleBaseAddress;

      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000140261bc9ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000129ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000140261aa0ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?removeAnyDuplicateLines@ScintillaEditView@@QEAAXXZ", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("ScintillaEditView.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(6199, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\ScintillaEditView.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x0000000000000129ul, cod_result.AddressInFunction);
      Assert.AreEqual(68749, cod_result.CodFileLineNumber);
      Assert.AreEqual(@"..\..\TestFiles\ScintillaEditView.cod", cod_result.CodFullPathName);
      Assert.AreEqual("ScintillaEditView::removeAnyDuplicateLines", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(3927, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\notepadplusplus\\notepad-plus-plus-master\\PowerEditor\\src\\ScintillaComponent\\ScintillaEditView.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0x00000002, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"3927 :   {
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
