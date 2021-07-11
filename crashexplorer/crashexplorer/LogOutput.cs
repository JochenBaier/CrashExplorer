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
using System.Windows.Forms;

namespace CrashExplorer
{
  /// <summary>
  /// Helper to the result output rich text box
  /// </summary>
  ///
  public class LogOutput
  {
    private readonly string m_empty_busy_box = "[     ]";
    private readonly string[] m_busy_box_frames = { "[.    ]", "[ .   ]", "[  .  ]", "[   . ]", "[    .]", "[   . ]", "[  .  ]", "[ .   ]", "[.    ]" };

    private readonly Timer m_animation_timer;
    private readonly RichTextBox m_richttextbox;
    private uint m_busy_animation_counter;
    private int m_log_ident = 8;
    private int m_busy_box_index;

    public LogOutput(RichTextBox richTextBox, Timer animationTimer)
    {
      m_richttextbox = richTextBox;
      m_animation_timer = animationTimer;
      m_animation_timer.Tick += TimerProgressAnimationTick;
    }

    public void Clear()
    {
      m_richttextbox.Clear();
    }

    public void AppendText(string text)
    {
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Regular);
      m_richttextbox.AppendText(text);
    }

    public void AppendBoldText(string text, bool prependBusyBox)
    {
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Bold);

      if (prependBusyBox)
      {
        m_richttextbox.AppendText(m_empty_busy_box + " ");
      }

      m_richttextbox.AppendText(text);
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Regular);
    }

    public void AppendErrorTextIndented(string text)
    {
      AppendBoldColorText("\n[Error]", Color.Red);

      string[] splitted = text.Split('\n');

      string ident_empyt = new String(' ', m_log_ident);
      AppendText(" " + string.Join("\n" + ident_empyt, splitted));
      AppendBoldColorText("\n\nAnalysis aborted.", Color.Red);
    }

    public void AppendBoldColorText(string text, Color color)
    {
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Bold);
      m_richttextbox.SelectionColor = color;

      m_richttextbox.AppendText(text);
      m_richttextbox.SelectionColor = m_richttextbox.ForeColor;
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Regular);
    }

    public void AppendBoldColorText(string text, float fontSize, Color color)
    {
      m_richttextbox.SelectionFont = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold);
      m_richttextbox.SelectionColor = color;

      m_richttextbox.AppendText(text);
      m_richttextbox.SelectionColor = m_richttextbox.ForeColor;
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Regular);
    }

    public void StartBusyAnimation()
    {
      m_busy_animation_counter = 0;
      m_busy_box_index = m_richttextbox.Text.LastIndexOf(m_empty_busy_box);
      Debug.Assert(m_busy_box_index != -1);
      m_animation_timer.Start();
    }

    public void StopBusyAnimation(bool finishedWithError)
    {
      m_animation_timer.Stop();
      SetBusyText(finishedWithError ? "[Error]" : "[Ok   ]");
      m_richttextbox.DeselectAll();
    }

    private void TimerProgressAnimationTick(object sender, EventArgs e)
    {
      m_busy_animation_counter++;
      uint animation_frame_index = m_busy_animation_counter % (uint)m_busy_box_frames.Length;
      SetBusyText(m_busy_box_frames[animation_frame_index]);
    }

    private void SetBusyText(string text)
    {
      m_richttextbox.SelectionStart = m_busy_box_index;
      m_richttextbox.SelectionLength = m_empty_busy_box.Length;
      m_richttextbox.SelectedText = text;
    }
  }
}
