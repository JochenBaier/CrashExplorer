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

using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CrashExplorer.library;

namespace CrashExplorer
{
  internal static class Analyzer
  {
    /// <summary>
    /// Parse map file, search cod file, parse cod file. Display result
    /// </summary>
    ///
    public static async Task StartAnalysing(FunctionResult functionResult, LogOutput logOutput, string mapFile,
      string codFolder, ulong offsetToFind, uint exceptionCode)
    {
      logOutput.AppendBoldColorText("Running Analysis...\n\n", 11, Color.DodgerBlue);

      logOutput.AppendBoldText("Parsing map file: '", true);
      logOutput.AppendText(mapFile + "'");

      logOutput.StartBusyAnimation();
      var map_file_results = await Task.Run(() => MapFileParser.ParseMapFileAsync(functionResult, mapFile, offsetToFind));
      logOutput.StopBusyAnimation(functionResult.IsBad);
      if (functionResult.IsBad)
      {
        return;
      }
      logOutput.AppendText("\n");
      await Task.Run(() => Thread.Sleep(500));

      string codFileName = Path.GetFileNameWithoutExtension(map_file_results.FileFunction.ObjectName) + ".cod";

      logOutput.AppendBoldText("Searching cod file '", true);
      logOutput.AppendText(codFileName + "' in folder '" + codFolder + "'");

      CodResult cod_result = null;
      FunctionResult functionResultCod = new FunctionResult();
      logOutput.StartBusyAnimation();
      string cod_file_full_path = await Task.Run(() => FileSystemHelper.FindCodFileInFolder(functionResultCod, codFolder, codFileName));
      logOutput.StopBusyAnimation(functionResultCod.IsBad);
      if (!functionResultCod.IsBad)
      {
        logOutput.AppendText("\n");
        await Task.Run(() => Thread.Sleep(500));

        logOutput.AppendBoldText("Parsing cod file: '", true);
        logOutput.AppendText(cod_file_full_path + "'");

        logOutput.StartBusyAnimation();
        cod_result =
          await Task.Run(() => CodFileParser.ParseCodFile(functionResultCod, cod_file_full_path, map_file_results));
        logOutput.StopBusyAnimation(functionResultCod.IsBad);
      }

      logOutput.AppendText("\n");
      logOutput.AppendBoldColorText("\nDone. Displaying results...\n", 11, Color.DodgerBlue);
      await Task.Run(() => Thread.Sleep(1000));
      logOutput.Clear();

      OutputHelper.ShowMapFileResult(logOutput, map_file_results, exceptionCode);
      OutputHelper.ShowCodFileResults(logOutput, functionResultCod, cod_file_full_path, cod_result);
    }
  }
}
