using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using crashexplorer_library;

namespace crashexplorer
{
  public class log_output
  {
    //FIXME rein statisch

    public log_output(RichTextBox p_richtextbox, string p_map_file)
    {
      m_richttextbox = p_richtextbox;
      m_map_file = p_map_file;
    }

    public void fill(analyse_result_t p_result)
    {
      m_richttextbox.Clear();

      //FIXME (@ 100)
      append_bold_text("Map file: '");
      append_text(m_map_file + "' (Line: " + p_result.m_map_file_line_number +")" +Environment.NewLine);

      //FIXME (@ 100)
      append_bold_text("Cod file: '");
      append_text(p_result.m_cod_full_path_name + "' (Line: " + p_result.m_cod_file_line_number + ")" +Environment.NewLine);

      append_bold_text("Library: ");
      append_text("'" + p_result.m_function_data.m_library_name + "' ");
      
      append_bold_text("Object: ");
      append_text("'" + p_result.m_function_data.m_object_name + "' ");
      append_text(Environment.NewLine);

      append_bold_text("Offset in module:");
      append_text(" '0x" + p_result.m_address_in_module.ToString("x") + "' ");

      append_bold_text("Offset in function:");
      append_text(" '0x" + p_result.m_address_in_function.ToString("x5") + "' ");
      append_text(Environment.NewLine);


      append_text(Environment.NewLine);

      append_bold_text("Source file: '");
      append_text(p_result.m_source_file_name +"'");
      append_text(Environment.NewLine);

      append_bold_text("Function: '");
      append_text(p_result.m_function_name_undecorated + "':");
      append_text(Environment.NewLine);
      

      string padding = "    ";

      append_text(padding + "..." + Environment.NewLine);

      for (int i = 0; i < p_result.m_source_code_block.Count; ++i)
      {
        append_text(padding + "@" + p_result.m_source_code_block[i] + Environment.NewLine);
      }
      append_text(padding + "..."+ Environment.NewLine);

      append_text(Environment.NewLine);
      //append_bold_text("Assembly: "+ Environment.NewLine);

      string mark_arrow = "--> ";
    
      //FIXME space vorne weg, tab dings beachten

      for (int i = 0; i < p_result.m_assembly_code_block.Count; ++i)
      {
        if (i == p_result.m_assembly_block_mark)
        {
          append_bold_color_text(mark_arrow + p_result.m_assembly_code_block[i] + Environment.NewLine, Color.Red);
        }
        else
        {
          append_text(padding+p_result.m_assembly_code_block[i] + Environment.NewLine);
        }

      }

     


    }

    private void append_text(string p_text)
    {
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Regular);
      m_richttextbox.AppendText(p_text);
    }
    private void append_bold_text(string p_text)
    {
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Bold);
      m_richttextbox.AppendText(p_text);
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Regular);
    }

    private void append_bold_color_text(string p_text, Color p_color)
    {
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Bold);
      m_richttextbox.SelectionColor = p_color;

      m_richttextbox.AppendText(p_text);
      m_richttextbox.SelectionColor = m_richttextbox.ForeColor;
      m_richttextbox.SelectionFont = new Font(m_richttextbox.Font, FontStyle.Regular);
    }


    private RichTextBox m_richttextbox;
    private string m_map_file;
  }
}
