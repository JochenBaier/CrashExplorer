using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CrashExplorer.library;

namespace UnitTest
{
  [TestClass]
  public class TestProjectCrashInDynamicLibA
  {
    [TestMethod]
    public void TestRelease()
    {
      var mapFile = @"..\..\TestFiles\release\dynamic_library.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x000000000000209dul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x000000018000209dul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x00000000000000ddul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000180000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000180001fc0ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?primMST@@YAXQEAY04H@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("spanning_tree.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(112, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\release\spanning_tree.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x00000000000000ddul, cod_result.AddressInFunction);
      Assert.AreEqual(680, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\release\\spanning_tree.cod", cod_result.CodFullPathName);
      Assert.AreEqual("primMST", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(27, cod_result.SourceFileLineNumber);
      
      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\dynamic_library\\spanning_tree.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);
      
      string expectedCodeBlock = @"27   :       volatile int* a = reinterpret_cast<volatile int*>(NULL);
28   :       *a = 1;//TEST crash: write acces violation";

        Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"000dd  44 89 34 25 00
00 00 00   mov   DWORD PTR ds:0, r14d
000e5  8b cf     mov   ecx, edi
000e7  4c 8b c3   mov   r8, rbx";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);

    }

    [TestMethod]
    public void TestDebug()
    {
      var mapFile = @"..\..\TestFiles\debug\dynamic_library.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000005deaul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000180005deaul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x00000000000000aaul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000180000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000180005d40ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?minKey@@YAHQEAHQEA_N@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("spanning_tree.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(226, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\debug\spanning_tree.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x00000000000000aaul, cod_result.AddressInFunction);
      Assert.AreEqual(1958, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\debug\\spanning_tree.cod", cod_result.CodFullPathName);
      Assert.AreEqual("minKey", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(28, cod_result.SourceFileLineNumber);

      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\dynamic_library\\spanning_tree.cpp", cod_result.SourceFileName);
      Assert.AreEqual(1, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"28   :       *a = 1;//TEST crash: write acces violation";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"000a5  48 8b 44 24 30   mov   rax, QWORD PTR a$3[rsp]
000aa  c7 00 01 00 00
00     mov   DWORD PTR [rax], 1";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }
  }
}
