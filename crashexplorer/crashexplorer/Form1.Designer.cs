
namespace CrashExplorer
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItemExceptionCode = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpAndAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBoxExceptionCode = new System.Windows.Forms.GroupBox();
      this.labelExceptionCode = new System.Windows.Forms.Label();
      this.textBoxExceptionCode = new System.Windows.Forms.TextBox();
      this.textBoxModuleBaseAddress = new System.Windows.Forms.TextBox();
      this.textBoxCallstackAddress = new System.Windows.Forms.TextBox();
      this.textBoxFaultOffset = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.radioButtonCallstackAddress = new System.Windows.Forms.RadioButton();
      this.radioButtonFaultOffset = new System.Windows.Forms.RadioButton();
      this.labelModulesBaseAddress = new System.Windows.Forms.Label();
      this.buttonSelectCodFolder = new System.Windows.Forms.Button();
      this.textBoxCodFolder = new System.Windows.Forms.TextBox();
      this.labelCodSearchFolder = new System.Windows.Forms.Label();
      this.buttonSelectMapFile = new System.Windows.Forms.Button();
      this.textBoxMapFile = new System.Windows.Forms.TextBox();
      this.labelMapFile = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.richTextBox_output = new System.Windows.Forms.RichTextBox();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.buttonQuit = new System.Windows.Forms.Button();
      this.buttonStart = new System.Windows.Forms.Button();
      this.timerProgressAnimation = new System.Windows.Forms.Timer(this.components);
      this.menuStrip1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBoxExceptionCode.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(1012, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // toolsToolStripMenuItem
      // 
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExceptionCode});
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
      this.toolsToolStripMenuItem.Text = "Tools";
      // 
      // toolStripMenuItemExceptionCode
      // 
      this.toolStripMenuItemExceptionCode.Name = "toolStripMenuItemExceptionCode";
      this.toolStripMenuItemExceptionCode.Size = new System.Drawing.Size(244, 22);
      this.toolStripMenuItemExceptionCode.Text = "Show Exception Code Converter";
      this.toolStripMenuItemExceptionCode.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAndAboutToolStripMenuItem,
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // helpAndAboutToolStripMenuItem
      // 
      this.helpAndAboutToolStripMenuItem.Name = "helpAndAboutToolStripMenuItem";
      this.helpAndAboutToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.helpAndAboutToolStripMenuItem.Text = "Project website";
      this.helpAndAboutToolStripMenuItem.Click += new System.EventHandler(this.helpAndAboutToolStripMenuItem_Click);
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
      this.aboutToolStripMenuItem.Text = "About";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.groupBoxExceptionCode);
      this.groupBox1.Controls.Add(this.textBoxModuleBaseAddress);
      this.groupBox1.Controls.Add(this.textBoxCallstackAddress);
      this.groupBox1.Controls.Add(this.textBoxFaultOffset);
      this.groupBox1.Controls.Add(this.panel1);
      this.groupBox1.Controls.Add(this.labelModulesBaseAddress);
      this.groupBox1.Controls.Add(this.buttonSelectCodFolder);
      this.groupBox1.Controls.Add(this.textBoxCodFolder);
      this.groupBox1.Controls.Add(this.labelCodSearchFolder);
      this.groupBox1.Controls.Add(this.buttonSelectMapFile);
      this.groupBox1.Controls.Add(this.textBoxMapFile);
      this.groupBox1.Controls.Add(this.labelMapFile);
      this.groupBox1.Location = new System.Drawing.Point(12, 24);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(988, 135);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Input";
      // 
      // groupBoxExceptionCode
      // 
      this.groupBoxExceptionCode.Controls.Add(this.labelExceptionCode);
      this.groupBoxExceptionCode.Controls.Add(this.textBoxExceptionCode);
      this.groupBoxExceptionCode.Location = new System.Drawing.Point(749, 84);
      this.groupBoxExceptionCode.Name = "groupBoxExceptionCode";
      this.groupBoxExceptionCode.Size = new System.Drawing.Size(232, 45);
      this.groupBoxExceptionCode.TabIndex = 21;
      this.groupBoxExceptionCode.TabStop = false;
      this.groupBoxExceptionCode.Text = "Optional";
      this.groupBoxExceptionCode.Visible = false;
      // 
      // labelExceptionCode
      // 
      this.labelExceptionCode.AutoSize = true;
      this.labelExceptionCode.Location = new System.Drawing.Point(4, 24);
      this.labelExceptionCode.Name = "labelExceptionCode";
      this.labelExceptionCode.Size = new System.Drawing.Size(112, 13);
      this.labelExceptionCode.TabIndex = 1;
      this.labelExceptionCode.Text = "Exception code [Hex]:";
      // 
      // textBoxExceptionCode
      // 
      this.textBoxExceptionCode.Location = new System.Drawing.Point(122, 20);
      this.textBoxExceptionCode.Name = "textBoxExceptionCode";
      this.textBoxExceptionCode.Size = new System.Drawing.Size(104, 20);
      this.textBoxExceptionCode.TabIndex = 0;
      this.textBoxExceptionCode.TextChanged += new System.EventHandler(this.textBoxExceptionCode_TextChanged);
      // 
      // textBoxModuleBaseAddress
      // 
      this.textBoxModuleBaseAddress.Enabled = false;
      this.textBoxModuleBaseAddress.Location = new System.Drawing.Point(486, 104);
      this.textBoxModuleBaseAddress.Name = "textBoxModuleBaseAddress";
      this.textBoxModuleBaseAddress.Size = new System.Drawing.Size(128, 20);
      this.textBoxModuleBaseAddress.TabIndex = 20;
      this.textBoxModuleBaseAddress.TextChanged += new System.EventHandler(this.hex_textBox_text_changed);
      // 
      // textBoxCallstackAddress
      // 
      this.textBoxCallstackAddress.Enabled = false;
      this.textBoxCallstackAddress.Location = new System.Drawing.Point(202, 104);
      this.textBoxCallstackAddress.Name = "textBoxCallstackAddress";
      this.textBoxCallstackAddress.Size = new System.Drawing.Size(119, 20);
      this.textBoxCallstackAddress.TabIndex = 19;
      this.textBoxCallstackAddress.TextChanged += new System.EventHandler(this.hex_textBox_text_changed);
      // 
      // textBoxFaultOffset
      // 
      this.textBoxFaultOffset.Location = new System.Drawing.Point(202, 79);
      this.textBoxFaultOffset.Name = "textBoxFaultOffset";
      this.textBoxFaultOffset.Size = new System.Drawing.Size(119, 20);
      this.textBoxFaultOffset.TabIndex = 18;
      this.textBoxFaultOffset.TextChanged += new System.EventHandler(this.hex_textBox_text_changed);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.radioButtonCallstackAddress);
      this.panel1.Controls.Add(this.radioButtonFaultOffset);
      this.panel1.Location = new System.Drawing.Point(11, 75);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(184, 50);
      this.panel1.TabIndex = 17;
      // 
      // radioButtonCallstackAddress
      // 
      this.radioButtonCallstackAddress.AutoSize = true;
      this.radioButtonCallstackAddress.Location = new System.Drawing.Point(1, 28);
      this.radioButtonCallstackAddress.Name = "radioButtonCallstackAddress";
      this.radioButtonCallstackAddress.Size = new System.Drawing.Size(144, 17);
      this.radioButtonCallstackAddress.TabIndex = 1;
      this.radioButtonCallstackAddress.Text = "Call Stack address [Hex]:";
      this.radioButtonCallstackAddress.UseVisualStyleBackColor = true;
      this.radioButtonCallstackAddress.CheckedChanged += new System.EventHandler(this.radioButton_callstack_address_CheckedChanged);
      // 
      // radioButtonFaultOffset
      // 
      this.radioButtonFaultOffset.AutoSize = true;
      this.radioButtonFaultOffset.Checked = true;
      this.radioButtonFaultOffset.Location = new System.Drawing.Point(1, 4);
      this.radioButtonFaultOffset.Name = "radioButtonFaultOffset";
      this.radioButtonFaultOffset.Size = new System.Drawing.Size(123, 17);
      this.radioButtonFaultOffset.TabIndex = 0;
      this.radioButtonFaultOffset.TabStop = true;
      this.radioButtonFaultOffset.Text = "Crash address [Hex]:";
      this.radioButtonFaultOffset.UseVisualStyleBackColor = true;
      this.radioButtonFaultOffset.CheckedChanged += new System.EventHandler(this.radioButton_fault_offset_CheckedChanged);
      // 
      // labelModulesBaseAddress
      // 
      this.labelModulesBaseAddress.AutoSize = true;
      this.labelModulesBaseAddress.Location = new System.Drawing.Point(329, 108);
      this.labelModulesBaseAddress.Name = "labelModulesBaseAddress";
      this.labelModulesBaseAddress.Size = new System.Drawing.Size(151, 13);
      this.labelModulesBaseAddress.TabIndex = 12;
      this.labelModulesBaseAddress.Text = ">  Module base address [Hex]:";
      // 
      // buttonSelectCodFolder
      // 
      this.buttonSelectCodFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSelectCodFolder.Location = new System.Drawing.Point(940, 44);
      this.buttonSelectCodFolder.Name = "buttonSelectCodFolder";
      this.buttonSelectCodFolder.Size = new System.Drawing.Size(41, 23);
      this.buttonSelectCodFolder.TabIndex = 5;
      this.buttonSelectCodFolder.Text = "...";
      this.buttonSelectCodFolder.UseVisualStyleBackColor = true;
      this.buttonSelectCodFolder.Click += new System.EventHandler(this.ButtonSelectCodFolderClick);
      // 
      // textBoxCodFolder
      // 
      this.textBoxCodFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxCodFolder.Location = new System.Drawing.Point(202, 45);
      this.textBoxCodFolder.Name = "textBoxCodFolder";
      this.textBoxCodFolder.Size = new System.Drawing.Size(732, 20);
      this.textBoxCodFolder.TabIndex = 4;
      this.textBoxCodFolder.TextChanged += new System.EventHandler(this.textBox_cod_folder_TextChanged);
      // 
      // labelCodSearchFolder
      // 
      this.labelCodSearchFolder.AutoSize = true;
      this.labelCodSearchFolder.Location = new System.Drawing.Point(8, 49);
      this.labelCodSearchFolder.Name = "labelCodSearchFolder";
      this.labelCodSearchFolder.Size = new System.Drawing.Size(187, 13);
      this.labelCodSearchFolder.TabIndex = 3;
      this.labelCodSearchFolder.Text = "Search listing files (*.cod) in this folder:";
      // 
      // buttonSelectMapFile
      // 
      this.buttonSelectMapFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonSelectMapFile.Location = new System.Drawing.Point(940, 17);
      this.buttonSelectMapFile.Name = "buttonSelectMapFile";
      this.buttonSelectMapFile.Size = new System.Drawing.Size(41, 23);
      this.buttonSelectMapFile.TabIndex = 2;
      this.buttonSelectMapFile.Text = "...";
      this.buttonSelectMapFile.UseVisualStyleBackColor = true;
      this.buttonSelectMapFile.Click += new System.EventHandler(this.ButtonSelectMapFileClick);
      // 
      // textBoxMapFile
      // 
      this.textBoxMapFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxMapFile.Location = new System.Drawing.Point(202, 18);
      this.textBoxMapFile.Name = "textBoxMapFile";
      this.textBoxMapFile.Size = new System.Drawing.Size(732, 20);
      this.textBoxMapFile.TabIndex = 1;
      this.textBoxMapFile.TextChanged += new System.EventHandler(this.textBox_map_file_TextChanged);
      // 
      // labelMapFile
      // 
      this.labelMapFile.AutoSize = true;
      this.labelMapFile.Location = new System.Drawing.Point(8, 22);
      this.labelMapFile.Name = "labelMapFile";
      this.labelMapFile.Size = new System.Drawing.Size(159, 13);
      this.labelMapFile.TabIndex = 0;
      this.labelMapFile.Text = "Faulting module map file (*.map):";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.richTextBox_output);
      this.groupBox2.Controls.Add(this.richTextBox1);
      this.groupBox2.Location = new System.Drawing.Point(12, 165);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(988, 437);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Output";
      // 
      // richTextBox_output
      // 
      this.richTextBox_output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.richTextBox_output.BackColor = System.Drawing.Color.White;
      this.richTextBox_output.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.richTextBox_output.Location = new System.Drawing.Point(6, 19);
      this.richTextBox_output.Name = "richTextBox_output";
      this.richTextBox_output.ReadOnly = true;
      this.richTextBox_output.Size = new System.Drawing.Size(976, 412);
      this.richTextBox_output.TabIndex = 2;
      this.richTextBox_output.Text = "";
      this.richTextBox_output.WordWrap = false;
      // 
      // richTextBox1
      // 
      this.richTextBox1.Location = new System.Drawing.Point(330, 141);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(8, 8);
      this.richTextBox1.TabIndex = 1;
      this.richTextBox1.Text = "";
      // 
      // buttonQuit
      // 
      this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonQuit.Location = new System.Drawing.Point(925, 608);
      this.buttonQuit.Name = "buttonQuit";
      this.buttonQuit.Size = new System.Drawing.Size(75, 23);
      this.buttonQuit.TabIndex = 3;
      this.buttonQuit.Text = "Quit";
      this.buttonQuit.UseVisualStyleBackColor = true;
      this.buttonQuit.Click += new System.EventHandler(this.button_quit_Click);
      // 
      // buttonStart
      // 
      this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonStart.Enabled = false;
      this.buttonStart.Location = new System.Drawing.Point(797, 608);
      this.buttonStart.Name = "buttonStart";
      this.buttonStart.Size = new System.Drawing.Size(122, 23);
      this.buttonStart.TabIndex = 4;
      this.buttonStart.Text = "Start Analysis";
      this.buttonStart.UseVisualStyleBackColor = true;
      this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
      // 
      // timerProgressAnimation
      // 
      this.timerProgressAnimation.Interval = 200;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1012, 639);
      this.Controls.Add(this.buttonStart);
      this.Controls.Add(this.buttonQuit);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.MinimumSize = new System.Drawing.Size(700, 500);
      this.Name = "Form1";
      this.Text = "CrashExplorer";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBoxExceptionCode.ResumeLayout(false);
      this.groupBoxExceptionCode.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label labelModulesBaseAddress;
    private System.Windows.Forms.Button buttonSelectCodFolder;
    private System.Windows.Forms.TextBox textBoxCodFolder;
    private System.Windows.Forms.Label labelCodSearchFolder;
    private System.Windows.Forms.Button buttonSelectMapFile;
    private System.Windows.Forms.TextBox textBoxMapFile;
    private System.Windows.Forms.Label labelMapFile;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button buttonQuit;
    private System.Windows.Forms.Button buttonStart;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.RadioButton radioButtonCallstackAddress;
    private System.Windows.Forms.RadioButton radioButtonFaultOffset;
    private System.Windows.Forms.RichTextBox richTextBox_output;
    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.TextBox textBoxModuleBaseAddress;
    private System.Windows.Forms.TextBox textBoxCallstackAddress;
    private System.Windows.Forms.TextBox textBoxFaultOffset;
    private System.Windows.Forms.Timer timerProgressAnimation;
    private System.Windows.Forms.ToolStripMenuItem helpAndAboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.GroupBox groupBoxExceptionCode;
    private System.Windows.Forms.Label labelExceptionCode;
    private System.Windows.Forms.TextBox textBoxExceptionCode;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExceptionCode;
  }
}

