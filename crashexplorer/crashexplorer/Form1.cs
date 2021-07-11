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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CrashExplorer.library;

namespace CrashExplorer
{
  public partial class Form1 : Form
  {
    private readonly LogOutput m_logOutPut;

    private readonly ToolTip m_textToolTip = new ToolTip();
    private ImageToolTip m_fault_offset_tool_tip;
    private ImageToolTip m_map_file_tool_tip;
    private ImageToolTip m_modules_base_address_tip;
    private ImageToolTip m_callstack_address_tip;
    private ImageToolTip m_exception_code_tip;

    public Form1()
    {
      InitializeComponent();
      InitTooltips();
      m_logOutPut = new LogOutput(richTextBox_output, timerProgressAnimation);
      OutputHelper.ShowQuickStart(m_logOutPut);
    }

    /// <summary>
    /// Start analysing after start button click
    /// </summary>
    ///
    private async void ButtonStartClick(object sender, EventArgs e)
    {
      m_logOutPut.Clear();

      Cursor.Current = Cursors.WaitCursor;
      buttonStart.Enabled = false;

      try
      {
        bool ok = GetOffsetToFind(out ulong offset_to_find);
        Debug.Assert(ok);

        var map_file = StringHelper.RemoveQuotes(textBoxMapFile.Text);
        var cod_folder = StringHelper.RemoveQuotes(textBoxCodFolder.Text);

        ulong exceptionCode = 0;
        if (!string.IsNullOrEmpty(textBoxExceptionCode.Text))
        {
          ok = StringHelper.ToHexNumber(textBoxExceptionCode.Text, out exceptionCode);
          Debug.Assert(ok);
        }

        FunctionResult functionResult = new FunctionResult();
        await Analyzer.StartAnalysing(functionResult, m_logOutPut, map_file, cod_folder, offset_to_find, (uint)exceptionCode);
        if (functionResult.IsBad)
        {
          m_logOutPut.AppendErrorTextIndented(functionResult.ErrorText);
        }
      }
      catch (Exception ex)
      {
        m_logOutPut.AppendErrorTextIndented(ex.Message);
        m_logOutPut.StopBusyAnimation(true);
      }
      finally
      {
        Cursor = Cursors.Default;
        buttonStart.Enabled = true;
      }
    }

    /// <summary>
    /// Helper
    /// </summary>
    ///
    private bool GetOffsetToFind(out ulong offset)
    {
      offset = 0;

      if (radioButtonFaultOffset.Checked)
      {
        bool ok = StringHelper.ToHexNumber(textBoxFaultOffset.Text, out offset);
        return ok;
      }
      else
      {
        bool ok = StringHelper.ToHexNumber(textBoxCallstackAddress.Text, out ulong callstack_address);
        if (!ok)
        {
          return false;
        }

        ok = StringHelper.ToHexNumber(textBoxModuleBaseAddress.Text, out ulong modul_base_address);
        if (!ok)
        {
          return false;
        }

        if (!(callstack_address > modul_base_address))
        {
          return false;
        }

        offset = callstack_address - modul_base_address;
        return true;
      }
    }

    /// <summary>
    /// Open file dialog to select map file
    /// </summary>
    ///
    private void ButtonSelectMapFileClick(object sender, EventArgs e)
    {
      OpenFileDialog open_file_dialog = new OpenFileDialog()
      {
        FileName = "Select a map file",
        Filter = "Map files (*.map)|*.map",
        Title = "Open map file"
      };

      if (open_file_dialog.ShowDialog() != DialogResult.OK)
      {
        return;
      }

      var filePath = open_file_dialog.FileName;
      textBoxMapFile.Text = filePath;
    }


    /// <summary>
    /// Open folder dialog to select base cod folder
    /// </summary>
    ///
    private void ButtonSelectCodFolderClick(object sender, EventArgs e)
    {
      using (var fbd = new FolderBrowserDialog())
      {
        DialogResult result = fbd.ShowDialog();

        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
        {
          textBoxCodFolder.Text = fbd.SelectedPath;
        }
      }
    }

    private void radioButton_fault_offset_CheckedChanged(object sender, EventArgs e)
    {
      radioButton_selection_changed();
    }

    private void radioButton_callstack_address_CheckedChanged(object sender, EventArgs e)
    {
      radioButton_selection_changed();
    }

    private void radioButton_selection_changed()
    {

      textBoxFaultOffset.Enabled = radioButtonFaultOffset.Checked;
      textBoxCallstackAddress.Enabled = !radioButtonFaultOffset.Checked;
      textBoxModuleBaseAddress.Enabled = !radioButtonFaultOffset.Checked;

      UpdateStartButtonState();
    }

    private void button_quit_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void hex_textBox_text_changed(object sender, EventArgs e)
    {
      TextBox textbox = (TextBox)sender;

      bool ok = StringHelper.ToHexNumber(textbox.Text, out _);
      textbox.ForeColor = ok ? Color.Black : Color.Red;
      UpdateStartButtonState();
    }

    private void textBox_map_file_TextChanged(object sender, EventArgs e)
    {
      UpdateStartButtonState();
    }

    private void textBox_cod_folder_TextChanged(object sender, EventArgs e)
    {
      UpdateStartButtonState();
    }

    private void textBoxExceptionCode_TextChanged(object sender, EventArgs e)
    {
      UpdateStartButtonState();
    }

    private void UpdateStartButtonState()
    {
      buttonStart.Enabled = false;

      if (!GetOffsetToFind(out _))
      {
        return;
      }

      if (textBoxMapFile.Text.Length < 3 || textBoxCodFolder.Text.Length < 3)
      {
        return;
      }

      if (!File.Exists(StringHelper.RemoveQuotes(textBoxMapFile.Text)) || !Directory.Exists(StringHelper.RemoveQuotes(textBoxCodFolder.Text)))
      {
        return;
      }

      if (!string.IsNullOrEmpty(textBoxExceptionCode.Text))
      {
        bool ok = StringHelper.ToHexNumber(textBoxExceptionCode.Text, out _);
        if (!ok)
        {
          return;
        }
      }

      buttonStart.Enabled = true;
    }

    private void helpAndAboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/JochenBaier/CrashExplorer/");
      Process.Start(sInfo);
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AboutBox1 a = new AboutBox1();
      a.Show();
    }

    private void toolStripMenuItem1_Click(object sender, EventArgs e)
    {
      if (toolStripMenuItemExceptionCode.Checked)
      {
        groupBoxExceptionCode.Visible = false;
        toolStripMenuItemExceptionCode.Checked = false;
      }
      else
      {
        groupBoxExceptionCode.Visible = true;
        toolStripMenuItemExceptionCode.Checked = true;
      }
    }

    private void InitTooltips()
    {
      m_textToolTip.SetToolTip(labelCodSearchFolder,
        "CrashExplorer will search the faulting 'translation-unit.cod' file in all subfolders of this folder.\r\nIn most cases this is the build folder.");

      m_textToolTip.SetToolTip(buttonSelectMapFile, "Select the map (*.map) file of the faulting module");
      m_textToolTip.SetToolTip(buttonSelectCodFolder, "Select the listing file (*.cod) base search folder");

      m_fault_offset_tool_tip = new ImageToolTip(new Bitmap(Properties.Resources.event_viewer_fault_offset));
      m_fault_offset_tool_tip.SetToolTip(radioButtonFaultOffset, "event_viewer_fault_offset");
      m_fault_offset_tool_tip.SetToolTip(textBoxFaultOffset, "event_viewer_fault_offset");

      m_map_file_tool_tip = new ImageToolTip(new Bitmap(Properties.Resources.event_viewer_faulting_module_name));
      m_map_file_tool_tip.SetToolTip(labelMapFile, "event_viewer_faulting_module_name");
      m_map_file_tool_tip.SetToolTip(textBoxMapFile, "event_viewer_faulting_module_name");

      m_modules_base_address_tip = new ImageToolTip(new Bitmap(Properties.Resources.visual_studio_modules_base_address));
      m_modules_base_address_tip.SetToolTip(labelModulesBaseAddress, "visual_studio_modules_base_address");
      m_modules_base_address_tip.SetToolTip(textBoxModuleBaseAddress, "visual_studio_modules_base_address");

      m_callstack_address_tip = new ImageToolTip(new Bitmap(Properties.Resources.visual_studio_call_stack_address));
      m_callstack_address_tip.SetToolTip(radioButtonCallstackAddress, "visual_studio_call_stack_address");
      m_callstack_address_tip.SetToolTip(textBoxModuleBaseAddress, "visual_studio_call_stack_address");

      m_exception_code_tip = new ImageToolTip(new Bitmap(Properties.Resources.event_viewer_exception_code));
      m_exception_code_tip.SetToolTip(labelExceptionCode, "event_viewer_exception_code");
      m_exception_code_tip.SetToolTip(textBoxExceptionCode, "event_viewer_exception_code");
    }
  }
}
