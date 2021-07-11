using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CrashExplorer.library;

namespace UnitTest
{
  [TestClass]
  public class TestProjectCrashInMain
  {
    [TestMethod]
    public void TestRelease()
    {
      var mapFile = @"..\..\TestFiles\release\test_project.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000001294ul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000140001294ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000004ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000140001290ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?function_in_main@@YAXXZ", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("main.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(87, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\release\main.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000000000004ul, cod_result.AddressInFunction);
      Assert.AreEqual(1080, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\release\\main.cod", cod_result.CodFullPathName);
      Assert.AreEqual("function_in_main", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(27, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\main\\main.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0x00000000, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"27   :   while (true)
28   :   {
29   :     std::array<uint8_t, 200> stack_memory_a;
30   :     function_in_main();  //TEST crash in main: stack overflow";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);
      string expectedAssemblyCodeBlock = @"00004  e8 00 00 00 00   call   ?function_in_main@@YAXXZ ; function_in_main";
      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }

    [TestMethod]
    public void TestDebug()
    {
      var mapFile = @"..\..\TestFiles\debug\test_project.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000002051ul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000140002051ul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x0000000000000021ul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000140000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000140002030ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?function_in_main@@YAXXZ", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("main.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(85, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\debug\main.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x0000000000000021ul, cod_result.AddressInFunction);
      Assert.AreEqual(1364, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\debug\\main.cod", cod_result.CodFullPathName);
      Assert.AreEqual("function_in_main", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(28, cod_result.SourceFileLineNumber);
      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\main\\main.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"28   :   {
29   :     std::array<uint8_t, 200> stack_memory_a;
30   :     function_in_main();  //TEST crash in main: stack overflow";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);
      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);
      string expectedAssemblyCodeBlock = @"00021  e8 00 00 00 00   call   ?function_in_main@@YAXXZ ; function_in_main";
      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);
    }
  }
}
