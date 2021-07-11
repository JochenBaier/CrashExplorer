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

using System;
using System.Drawing;
using CrashExplorer.library;

namespace CrashExplorer
{
  /// <summary>
  /// Helper to display results the output
  /// </summary>
  ///
  internal static class OutputHelper
  {
    public static void ShowMapFileResult(LogOutput logOutput, MapFileResults mapFileResults, uint exceptionCode)
    {
      logOutput.AppendBoldColorText("Map file results:\n", 11, Color.DodgerBlue);

      if (exceptionCode != 0)
      {
        string exceptionCodeAsString = ExceptionCodes.ToString(exceptionCode);
        logOutput.AppendBoldText("\nException code: '", false);
        logOutput.AppendText(exceptionCodeAsString + "'");
      }

      logOutput.AppendBoldText("\nMap file: ", false);
      logOutput.AppendText("'" + $"{mapFileResults.MapFilePath}" + "' ");
      logOutput.AppendText("(Line " + mapFileResults.FileFunction.MapFileLineNumber + ")");

      logOutput.AppendBoldText("\nAddress: ", false);
      logOutput.AppendText("'" + $"0x{mapFileResults.AddressToSearch:x}" + "' ");

      logOutput.AppendBoldText("\nFunction: ", false);
      logOutput.AppendText("'" + ParseFileHelper.GetUndecoratedFunctionName(mapFileResults.FileFunction.FunctionName) + "'");

      logOutput.AppendBoldText("\nLibrary: ", false);
      logOutput.AppendText("'" + mapFileResults.FileFunction.LibraryName + "'");
      logOutput.AppendBoldText(" Object: ", false);
      logOutput.AppendText("'" + mapFileResults.FileFunction.ObjectName + "'\n\n");
    }

    public static void ShowCodFileResults(LogOutput logOutput, FunctionResult functionResult, string codFileFullPath, CodResult codResult)
    {
      logOutput.AppendBoldColorText("Listing file results:\n", 11, Color.DodgerBlue);

      if (functionResult.IsBad)
      {
        logOutput.AppendBoldText("\nCod file: ", false);
        logOutput.AppendText("'" + $"{codFileFullPath}" + "' ");
        logOutput.AppendBoldColorText("\nError:\n", Color.Red);
        logOutput.AppendText(functionResult.ErrorText);
        logOutput.AppendBoldColorText("\n\nAnalysis aborted.", Color.Red);
        return;
      }

      logOutput.AppendBoldText("\nCod file: ", false);
      logOutput.AppendText("'" + $"{codResult.CodFullPathName}" + "' ");
      logOutput.AppendText("(Line " + codResult.CodFileLineNumber + ")");

      logOutput.AppendBoldText("\nAddress within function:", false);
      logOutput.AppendText(" '0x" + codResult.AddressInFunction.ToString("x5") + "' ");
      logOutput.AppendText(Environment.NewLine);

      logOutput.AppendBoldText("Source file: ", false);
      logOutput.AppendText("'" + codResult.SourceFileName + "' (Line: " + codResult.SourceFileLineNumber + ")\n");
      logOutput.AppendBoldText("Function: ", false);
      logOutput.AppendText("'" + codResult.FunctionNameUndecorated + "'\n");

      string padding = "    ";

      logOutput.AppendBoldText("\nSource code:\n\n", false);
      foreach (var sourceCodeLine in codResult.SourceCodeBlock)
      {
        logOutput.AppendText(padding + sourceCodeLine + Environment.NewLine);
      }

      logOutput.AppendText(Environment.NewLine);
      logOutput.AppendBoldText("Assembly, Machine Code:\n", false);

      logOutput.AppendText(Environment.NewLine);

      for (int i = 0; i < codResult.AssemblyCodeBlock.Count; ++i)
      {
        if (i == codResult.AssemblyBlockMark)
        {
          string arrow = "--> ";
          logOutput.AppendBoldColorText(arrow + codResult.AssemblyCodeBlock[i] + Environment.NewLine, Color.Red);
        }
        else
        {
          logOutput.AppendText(padding + codResult.AssemblyCodeBlock[i] + Environment.NewLine);
        }
      }
    }

    public static void ShowQuickStart(LogOutput logOutput)
    {
      logOutput.AppendBoldColorText("Quick Start:\n\n", 11, Color.DodgerBlue);

      logOutput.AppendBoldText("Visual Studio settings:", false);
      logOutput.AppendText("\n1. Enable map file: 'Configuration Properties->Linker->Debugging->Generate Map File' ('/MAP') for the faulting module (exe or dll).");
      logOutput.AppendText("\n2. Enable listing file: 'Configuration Properties->C/C++->Output Files->Assembler Output': 'Assembly, Machine Code and Source (/FAcs)'");

      logOutput.AppendBoldText("\n\nEvent Viewer:", false);
      logOutput.AppendText("\nCopy 'Fault offset' from the Application Error Log entry to 'Crash address' input box.");
    }
  }
}