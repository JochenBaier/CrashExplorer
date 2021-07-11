using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CrashExplorer.library;

namespace UnitTest
{
  [TestClass]
  public class TestProjectCrashInDynamicLibB
  {
    [TestMethod]
    public void TestRelease()
    {
      var mapFile = @"..\..\TestFiles\release\dynamic_library.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000001a0ful;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000180001a0ful, map_file_results.AddressToSearch);
      Assert.AreEqual(0x00000000000001eful, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000180000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000180001820ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?convexHull@Cmathlibrary@@AEAAXQEAUPoint@@H@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("math_library.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(106, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\release\math_library.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x00000000000001eful, cod_result.AddressInFunction);
      Assert.AreEqual(3385, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\release\\math_library.cod", cod_result.CodFullPathName);
      Assert.AreEqual("Cmathlibrary::convexHull", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(20, cod_result.SourceFileLineNumber);
      
      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\dynamic_library\\math_library.cpp", cod_result.SourceFileName);
      Assert.AreEqual(1, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);
      
      string expectedCodeBlock = @"20   :   *((unsigned int*)0) = 0xDEAD; //TEST crash: write access violation";

        Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"001ea  b8 ad de 00 00   mov   eax, 57005    ; 0000deadH
001ef  a3 00 00 00 00
00 00 00 00   mov   DWORD PTR ds:0, eax";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);

    }

    [TestMethod]
    public void TestDebug()
    {
      var mapFile = @"..\..\TestFiles\debug\dynamic_library.map";
      Assert.IsTrue(File.Exists(mapFile));

      FunctionResult functionResult = new FunctionResult();
      ulong offsetToFind = 0x0000000000001c0cul;
      var map_file_results = MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind);
      Assert.IsFalse(functionResult.IsBad);

      Assert.AreEqual(0x0000000180001c0cul, map_file_results.AddressToSearch);
      Assert.AreEqual(0x000000000000004cul, map_file_results.AddressWithinFunction);
      Assert.AreEqual(0x0000000180000000ul, map_file_results.PreferredLoadAddress);
      Assert.AreEqual(0x0000000180001bc0ul, map_file_results.FileFunction.Address);
      Assert.AreEqual("?nextToTop@Cmathlibrary@@AEAA?AUPoint@@AEAV?$stack@UPoint@@V?$deque@UPoint@@V?$allocator@UPoint@@@std@@@std@@@std@@@Z", map_file_results.FileFunction.FunctionName);
      Assert.AreEqual(null, map_file_results.FileFunction.LibraryName);
      Assert.AreEqual("math_library.obj", map_file_results.FileFunction.ObjectName);
      Assert.AreEqual(79, map_file_results.FileFunction.MapFileLineNumber);

      var codFile = @"..\..\TestFiles\debug\math_library.cod";
      Assert.IsTrue(File.Exists(codFile));

      CodResult cod_result = CodFileParser.ParseCodFile(functionResult, codFile, map_file_results);
      Assert.IsFalse(functionResult.IsBad);


      Assert.AreEqual(0x000000000000004cul, cod_result.AddressInFunction);
      Assert.AreEqual(9331, cod_result.CodFileLineNumber);
      Assert.AreEqual("..\\..\\TestFiles\\debug\\math_library.cod", cod_result.CodFullPathName);
      Assert.AreEqual("Cmathlibrary::nextToTop", cod_result.FunctionNameUndecorated);
      Assert.AreEqual(19, cod_result.SourceFileLineNumber);

      Assert.AreEqual("D:\\dev\\crashexplorer\\crashexplorer\\test_projects\\test_project\\dynamic_library\\math_library.cpp", cod_result.SourceFileName);
      Assert.AreEqual(0, cod_result.AssemblyBlockMark);

      string sourceCodeBlock = string.Join("\r\n", cod_result.SourceCodeBlock);

      string expectedCodeBlock = @"19   :     
20   :   *((unsigned int*)0) = 0xDEAD; //TEST crash: write access violation";

      Assert.AreEqual(expectedCodeBlock, sourceCodeBlock);

      string assemblyCodeBlock = string.Join("\r\n", cod_result.AssemblyCodeBlock);

      string expectedAssemblyCodeBlock = @"0004c  c7 04 25 00 00
00 00 ad de 00
00     mov   DWORD PTR ds:0, 57005  ; 0000deadH";

      Assert.AreEqual(expectedAssemblyCodeBlock, assemblyCodeBlock);

    }
  }
}
