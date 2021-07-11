using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CrashExplorer.library;

namespace UnitTest
{
  [TestClass]
  public class TestProjectCrashInStaticLibA
  {
    [TestMethod]
    public void TestRelease()
    {
      var mapFile = @"..\..\TestFiles\release\test_project.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000001467ul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000140001467ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x00000000000000d7ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000140001390ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("main", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("main.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(90, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\release\main.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x00000000000000d7ul, cod_result.AddressInFunction);
      Assert.AreEqual(813, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\release\\main.cod", cod_result.CodFullPathName);
      Assert.AreEqual("main", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(25, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\main\\main.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0x00000000, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"25   :       
26   :       int* a = nullptr;
27   :       *a = 42; //TEST crash: write access violation";

        Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"000d7  c7 03 2a 00 00
00     mov   DWORD PTR [rbx], 42  ; 0000002aH";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);

    }

    [TestMethod]
    public void TestDebug()
    {
      var mapFile = @"..\..\TestFiles\debug\test_project.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000001ed9ul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000140001ed9ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000079ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000140001e60ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?partition@@YAHQEAHHH@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("main.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(82, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\debug\main.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x0000000000000079ul, cod_result.AddressInFunction);
      Assert.AreEqual(2345, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\debug\\main.cod", cod_result.CodFullPathName);
      Assert.AreEqual("partition", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(27, cod_result.SourceFileLineNumber);
      
      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\static_library\\quick_sort.cpp", cod_result.SourceFileName);
      Assert.AreEqual(1, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"27   :       *a = 42; //TEST crash: write access violation";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"00074  48 8b 44 24 30   mov   rax, QWORD PTR a$2[rsp]
00079  c7 00 2a 00 00
00     mov   DWORD PTR [rax], 42  ; 0000002aH";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);

    }
  }
}
