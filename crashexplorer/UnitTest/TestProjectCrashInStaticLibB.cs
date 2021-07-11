using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CrashExplorer.library;

namespace UnitTest
{
  [TestClass]
  public class TestProjectCrashInStaticLibB
  {
    [TestMethod]
    public void TestRelease()
    {
      var mapFile = @"..\..\TestFiles\release\test_project.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000001d89ul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000140001d89ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000099ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000140001cf0ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?buildMatchingMachine@pattern_searcher_t@@AEAAHQEAV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@H@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual("static_library", map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("pattern_searcher.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(112, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\release\pattern_searcher.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x0000000000000099ul, cod_result.AddressInFunction);
      Assert.AreEqual(4247, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\release\\pattern_searcher.cod", cod_result.CodFullPathName);
      Assert.AreEqual("pattern_searcher_t::buildMatchingMachine", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(35, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\static_library\\pattern_searcher.cpp", cod_result.SourceFileName);
      Assert.AreEqual(3, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"35   :       int ch = word[j] - 'a' /j; //TEST crash: integer divide by 0";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"0008e  45 0f be 04 03   movsx   r8d, BYTE PTR [r11+rax]
00093  b8 61 00 00 00   mov   eax, 97      ; 00000061H
00098  99     cdq
00099  41 f7 fa   idiv   r10d
0009c  44 2b c0   sub   r8d, eax";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }

    [TestMethod]
    public void TestDebug()
    {
      var mapFile = @"..\..\TestFiles\debug\test_project.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x000000000000319cul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x000000014000319cul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x000000000000011cul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000140003080ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?buildMatchingMachine@pattern_searcher_t@@AEAAHQEAV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@H@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual("static_library", map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("pattern_searcher.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(107, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\debug\pattern_searcher.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x000000000000011cul, cod_result.AddressInFunction);
      Assert.AreEqual(11412, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\debug\\pattern_searcher.cod", cod_result.CodFullPathName);
      Assert.AreEqual("pattern_searcher_t::buildMatchingMachine", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(34, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\static_library\\pattern_searcher.cpp", cod_result.SourceFileName);
      Assert.AreEqual(9, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"34   :     {
35   :       int ch = word[j] - 'a' /j; //TEST crash: integer divide by 0";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"000fa  48 63 44 24 34   movsxd   rax, DWORD PTR j$7[rsp]
000ff  48 8b d0   mov   rdx, rax
00102  48 8b 4c 24 28   mov   rcx, QWORD PTR word$5[rsp]
00107  e8 00 00 00 00   call   ??A?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@QEBAAEBD_K@Z ; std::basic_string<char,std::char_traits<char>,std::allocator<char> >::operator[]
0010c  0f be 00   movsx   eax, BYTE PTR [rax]
0010f  89 84 24 90 00
00 00     mov   DWORD PTR tv86[rsp], eax
00116  b8 61 00 00 00   mov   eax, 97      ; 00000061H
0011b  99     cdq
0011c  f7 7c 24 34   idiv   DWORD PTR j$7[rsp]
00120  8b 8c 24 90 00
00 00     mov   ecx, DWORD PTR tv86[rsp]
00127  2b c8     sub   ecx, eax
00129  8b c1     mov   eax, ecx
0012b  89 44 24 38   mov   DWORD PTR ch$8[rsp], eax";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }
  }
}
