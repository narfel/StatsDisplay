namespace StatsDisplay
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            RunningStatusLabel = new ToolStripStatusLabel();
            toolStripStatusLabelSpring = new ToolStripStatusLabel();
            ToolStripStatusLabelPort = new ToolStripStatusLabel();
            ToolStripStatusLabelPortText = new ToolStripStatusLabel();
            ToolStripStatusLabelBR = new ToolStripStatusLabel();
            ToolStripStatusLabelBRText = new ToolStripStatusLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tabControl1 = new TabControl();
            ControlTab = new TabPage();
            groupBox2 = new GroupBox();
            OutputLabel = new Label();
            StartButton = new Button();
            SourceDataGroupBox = new GroupBox();
            DataListView = new ListView();
            Item = new ColumnHeader();
            Data = new ColumnHeader();
            ConfigTab = new TabPage();
            TempSelectButton = new Button();
            StartMinimizedCheckBox = new CheckBox();
            WinStartCheckBox = new CheckBox();
            DebugButton = new Button();
            ResetButton = new Button();
            AutorunCheckBox = new CheckBox();
            SaveButton = new Button();
            BaudrateComboBox = new ComboBox();
            BaudrateLabel = new Label();
            RefreshButton = new Button();
            PortComboBox = new ComboBox();
            ComportLabel = new Label();
            DebugTab = new TabPage();
            OutputBox = new TextBox();
            TempTab = new TabPage();
            TreeView = new TreeView();
            AboutTab = new TabPage();
            AboutLabel2 = new Label();
            label1 = new Label();
            linkLabel1 = new LinkLabel();
            TrayIcon = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            statusStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            ControlTab.SuspendLayout();
            groupBox2.SuspendLayout();
            SourceDataGroupBox.SuspendLayout();
            ConfigTab.SuspendLayout();
            DebugTab.SuspendLayout();
            TempTab.SuspendLayout();
            AboutTab.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, RunningStatusLabel, toolStripStatusLabelSpring, ToolStripStatusLabelPort, ToolStripStatusLabelPortText, ToolStripStatusLabelBR, ToolStripStatusLabelBRText });
            statusStrip1.Location = new Point(0, 183);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(426, 24);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(48, 19);
            toolStripStatusLabel1.Text = "  Status:";
            toolStripStatusLabel1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // RunningStatusLabel
            // 
            RunningStatusLabel.Name = "RunningStatusLabel";
            RunningStatusLabel.Size = new Size(111, 19);
            RunningStatusLabel.Spring = true;
            RunningStatusLabel.Text = "Idle";
            RunningStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelSpring
            // 
            toolStripStatusLabelSpring.Name = "toolStripStatusLabelSpring";
            toolStripStatusLabelSpring.Size = new Size(111, 19);
            toolStripStatusLabelSpring.Spring = true;
            // 
            // ToolStripStatusLabelPort
            // 
            ToolStripStatusLabelPort.BorderSides = ToolStripStatusLabelBorderSides.Left;
            ToolStripStatusLabelPort.Name = "ToolStripStatusLabelPort";
            ToolStripStatusLabelPort.Size = new Size(36, 19);
            ToolStripStatusLabelPort.Text = "Port:";
            ToolStripStatusLabelPort.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ToolStripStatusLabelPortText
            // 
            ToolStripStatusLabelPortText.Name = "ToolStripStatusLabelPortText";
            ToolStripStatusLabelPortText.Size = new Size(0, 19);
            ToolStripStatusLabelPortText.TextAlign = ContentAlignment.MiddleLeft;
            ToolStripStatusLabelPortText.ToolTipText = "No port set";
            // 
            // ToolStripStatusLabelBR
            // 
            ToolStripStatusLabelBR.BorderSides = ToolStripStatusLabelBorderSides.Left;
            ToolStripStatusLabelBR.Name = "ToolStripStatusLabelBR";
            ToolStripStatusLabelBR.Size = new Size(61, 19);
            ToolStripStatusLabelBR.Text = "Baudrate:";
            ToolStripStatusLabelBR.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ToolStripStatusLabelBRText
            // 
            ToolStripStatusLabelBRText.AutoSize = false;
            ToolStripStatusLabelBRText.Name = "ToolStripStatusLabelBRText";
            ToolStripStatusLabelBRText.Size = new Size(44, 19);
            ToolStripStatusLabelBRText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Location = new Point(0, 1);
            tableLayoutPanel1.Margin = new Padding(0, 0, 3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(0, 0);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(ControlTab);
            tabControl1.Controls.Add(ConfigTab);
            tabControl1.Controls.Add(DebugTab);
            tabControl1.Controls.Add(TempTab);
            tabControl1.Controls.Add(AboutTab);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(0);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(0, 0);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(426, 184);
            tabControl1.TabIndex = 3;
            // 
            // ControlTab
            // 
            ControlTab.Controls.Add(groupBox2);
            ControlTab.Controls.Add(SourceDataGroupBox);
            ControlTab.Location = new Point(4, 24);
            ControlTab.Name = "ControlTab";
            ControlTab.Padding = new Padding(3);
            ControlTab.Size = new Size(418, 156);
            ControlTab.TabIndex = 0;
            ControlTab.Text = "Control";
            ControlTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(OutputLabel);
            groupBox2.Controls.Add(StartButton);
            groupBox2.Location = new Point(170, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(241, 144);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Display Control";
            // 
            // OutputLabel
            // 
            OutputLabel.Location = new Point(6, 97);
            OutputLabel.Name = "OutputLabel";
            OutputLabel.Size = new Size(229, 44);
            OutputLabel.TabIndex = 2;
            OutputLabel.Text = "Settings Loaded";
            OutputLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(6, 18);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(229, 76);
            StartButton.TabIndex = 0;
            StartButton.Text = "Turn display on";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // SourceDataGroupBox
            // 
            SourceDataGroupBox.Controls.Add(DataListView);
            SourceDataGroupBox.Location = new Point(8, 5);
            SourceDataGroupBox.Name = "SourceDataGroupBox";
            SourceDataGroupBox.Size = new Size(156, 145);
            SourceDataGroupBox.TabIndex = 1;
            SourceDataGroupBox.TabStop = false;
            SourceDataGroupBox.Text = "System Stats";
            // 
            // DataListView
            // 
            DataListView.BorderStyle = BorderStyle.None;
            DataListView.Columns.AddRange(new ColumnHeader[] { Item, Data });
            DataListView.Dock = DockStyle.Fill;
            DataListView.HeaderStyle = ColumnHeaderStyle.None;
            DataListView.Location = new Point(3, 19);
            DataListView.Name = "DataListView";
            DataListView.Scrollable = false;
            DataListView.Size = new Size(150, 123);
            DataListView.TabIndex = 0;
            DataListView.UseCompatibleStateImageBehavior = false;
            DataListView.View = View.Details;
            // 
            // ConfigTab
            // 
            ConfigTab.Controls.Add(TempSelectButton);
            ConfigTab.Controls.Add(StartMinimizedCheckBox);
            ConfigTab.Controls.Add(WinStartCheckBox);
            ConfigTab.Controls.Add(DebugButton);
            ConfigTab.Controls.Add(ResetButton);
            ConfigTab.Controls.Add(AutorunCheckBox);
            ConfigTab.Controls.Add(SaveButton);
            ConfigTab.Controls.Add(BaudrateComboBox);
            ConfigTab.Controls.Add(BaudrateLabel);
            ConfigTab.Controls.Add(RefreshButton);
            ConfigTab.Controls.Add(PortComboBox);
            ConfigTab.Controls.Add(ComportLabel);
            ConfigTab.Location = new Point(4, 24);
            ConfigTab.Name = "ConfigTab";
            ConfigTab.Padding = new Padding(3);
            ConfigTab.Size = new Size(418, 156);
            ConfigTab.TabIndex = 1;
            ConfigTab.Text = "Config";
            ConfigTab.UseVisualStyleBackColor = true;
            // 
            // TempSelectButton
            // 
            TempSelectButton.Location = new Point(159, 119);
            TempSelectButton.Name = "TempSelectButton";
            TempSelectButton.Size = new Size(116, 23);
            TempSelectButton.TabIndex = 11;
            TempSelectButton.Text = "Select CPU Temp";
            TempSelectButton.UseVisualStyleBackColor = true;
            TempSelectButton.Click += TempSelectButton_Click;
            // 
            // StartMinimizedCheckBox
            // 
            StartMinimizedCheckBox.AutoSize = true;
            StartMinimizedCheckBox.Location = new Point(267, 93);
            StartMinimizedCheckBox.Name = "StartMinimizedCheckBox";
            StartMinimizedCheckBox.Size = new Size(109, 19);
            StartMinimizedCheckBox.TabIndex = 10;
            StartMinimizedCheckBox.Text = "Start minimized";
            StartMinimizedCheckBox.UseVisualStyleBackColor = true;
            // 
            // WinStartCheckBox
            // 
            WinStartCheckBox.AutoSize = true;
            WinStartCheckBox.Location = new Point(37, 93);
            WinStartCheckBox.Name = "WinStartCheckBox";
            WinStartCheckBox.Size = new Size(224, 19);
            WinStartCheckBox.TabIndex = 9;
            WinStartCheckBox.Text = "Run application when Windows starts";
            WinStartCheckBox.UseVisualStyleBackColor = true;
            WinStartCheckBox.CheckedChanged += WinStartCheckBox_CheckedChanged;
            // 
            // DebugButton
            // 
            DebugButton.Location = new Point(37, 119);
            DebugButton.Name = "DebugButton";
            DebugButton.Size = new Size(116, 23);
            DebugButton.TabIndex = 8;
            DebugButton.Text = "Show Output";
            DebugButton.UseVisualStyleBackColor = true;
            DebugButton.Click += DebugButton_Click;
            // 
            // ResetButton
            // 
            ResetButton.Location = new Point(231, 44);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(121, 23);
            ResetButton.TabIndex = 7;
            ResetButton.Text = "Clear Fields";
            ResetButton.UseVisualStyleBackColor = true;
            ResetButton.Click += ResetButton_Click;
            // 
            // AutorunCheckBox
            // 
            AutorunCheckBox.AutoSize = true;
            AutorunCheckBox.Location = new Point(37, 74);
            AutorunCheckBox.Name = "AutorunCheckBox";
            AutorunCheckBox.Size = new Size(247, 19);
            AutorunCheckBox.TabIndex = 6;
            AutorunCheckBox.Text = "Run automatically when application starts";
            AutorunCheckBox.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(312, 118);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 5;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // BaudrateComboBox
            // 
            BaudrateComboBox.FormattingEnabled = true;
            BaudrateComboBox.Location = new Point(104, 45);
            BaudrateComboBox.Name = "BaudrateComboBox";
            BaudrateComboBox.Size = new Size(121, 23);
            BaudrateComboBox.TabIndex = 4;
            // 
            // BaudrateLabel
            // 
            BaudrateLabel.AutoSize = true;
            BaudrateLabel.Location = new Point(37, 48);
            BaudrateLabel.Name = "BaudrateLabel";
            BaudrateLabel.Size = new Size(57, 15);
            BaudrateLabel.TabIndex = 3;
            BaudrateLabel.Text = "Baudrate:";
            // 
            // RefreshButton
            // 
            RefreshButton.Location = new Point(231, 16);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(121, 23);
            RefreshButton.TabIndex = 2;
            RefreshButton.Text = "Refresh ports";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Click += RefreshButton_Click;
            // 
            // PortComboBox
            // 
            PortComboBox.FormattingEnabled = true;
            PortComboBox.Location = new Point(104, 16);
            PortComboBox.Name = "PortComboBox";
            PortComboBox.Size = new Size(121, 23);
            PortComboBox.TabIndex = 1;
            // 
            // ComportLabel
            // 
            ComportLabel.AutoSize = true;
            ComportLabel.Location = new Point(37, 19);
            ComportLabel.Name = "ComportLabel";
            ComportLabel.Size = new Size(61, 15);
            ComportLabel.TabIndex = 0;
            ComportLabel.Text = "Com Port:";
            // 
            // DebugTab
            // 
            DebugTab.Controls.Add(OutputBox);
            DebugTab.Location = new Point(4, 24);
            DebugTab.Name = "DebugTab";
            DebugTab.Size = new Size(418, 156);
            DebugTab.TabIndex = 2;
            DebugTab.Text = "Output";
            DebugTab.UseVisualStyleBackColor = true;
            // 
            // OutputBox
            // 
            OutputBox.Anchor = AnchorStyles.None;
            OutputBox.BorderStyle = BorderStyle.None;
            OutputBox.Location = new Point(0, 0);
            OutputBox.Multiline = true;
            OutputBox.Name = "OutputBox";
            OutputBox.ScrollBars = ScrollBars.Vertical;
            OutputBox.Size = new Size(418, 156);
            OutputBox.TabIndex = 0;
            // 
            // TempTab
            // 
            TempTab.Controls.Add(TreeView);
            TempTab.Location = new Point(4, 24);
            TempTab.Name = "TempTab";
            TempTab.Size = new Size(418, 156);
            TempTab.TabIndex = 3;
            TempTab.Text = "Temp";
            TempTab.UseVisualStyleBackColor = true;
            // 
            // TreeView
            // 
            TreeView.Dock = DockStyle.Fill;
            TreeView.Location = new Point(0, 0);
            TreeView.Name = "TreeView";
            TreeView.Size = new Size(418, 156);
            TreeView.TabIndex = 12;
            TreeView.AfterSelect += TreeView_AfterSelect;
            // 
            // AboutTab
            // 
            AboutTab.Controls.Add(AboutLabel2);
            AboutTab.Controls.Add(label1);
            AboutTab.Controls.Add(linkLabel1);
            AboutTab.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            AboutTab.Location = new Point(4, 24);
            AboutTab.Name = "AboutTab";
            AboutTab.Size = new Size(418, 156);
            AboutTab.TabIndex = 4;
            AboutTab.Text = "About";
            AboutTab.UseVisualStyleBackColor = true;
            // 
            // AboutLabel2
            // 
            AboutLabel2.AutoSize = true;
            AboutLabel2.Location = new Point(16, 41);
            AboutLabel2.Name = "AboutLabel2";
            AboutLabel2.Size = new Size(141, 15);
            AboutLabel2.TabIndex = 2;
            AboutLabel2.Text = "Copyright (C) 2023 Narfel";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(16, 15);
            label1.Name = "label1";
            label1.Size = new Size(108, 15);
            label1.TabIndex = 1;
            label1.Text = "StatsDisplay v1.02";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(16, 56);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(210, 15);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://github.com/narfel/statsdisplay";
            // 
            // TrayIcon
            // 
            TrayIcon.Icon = (Icon)resources.GetObject("TrayIcon.Icon");
            TrayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(426, 207);
            Controls.Add(tabControl1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new Size(0, 246);
            Name = "Form1";
            Text = "Stats";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Shown += Form1_Shown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            ControlTab.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            SourceDataGroupBox.ResumeLayout(false);
            ConfigTab.ResumeLayout(false);
            ConfigTab.PerformLayout();
            DebugTab.ResumeLayout(false);
            DebugTab.PerformLayout();
            TempTab.ResumeLayout(false);
            AboutTab.ResumeLayout(false);
            AboutTab.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private TableLayoutPanel tableLayoutPanel1;
        private TabControl tabControl1;
        private TabPage ControlTab;
        private TabPage ConfigTab;
        private TabPage DebugTab;
        private Button StartButton;
        private Label BaudrateLabel;
        private Button RefreshButton;
        private ComboBox PortComboBox;
        private Label ComportLabel;
        private CheckBox AutorunCheckBox;
        private Button SaveButton;
        private ComboBox BaudrateComboBox;
        private ToolStripStatusLabel ToolStripStatusLabelPort;
        private ToolStripStatusLabel ToolStripStatusLabelPortText;
        private ToolStripStatusLabel ToolStripStatusLabelBR;
        private ToolStripStatusLabel ToolStripStatusLabelBRText;
        private ToolStripStatusLabel toolStripStatusLabelSpring;
        private GroupBox SourceDataGroupBox;
        private ListView DataListView;
        private ColumnHeader Item;
        private ColumnHeader Data;
        private Button ResetButton;
        private GroupBox groupBox2;
        private ToolStripStatusLabel RunningStatusLabel;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TextBox OutputBox;
        private Button DebugButton;
        private Label OutputLabel;
        private NotifyIcon TrayIcon;
        private ContextMenuStrip contextMenuStrip1;
        private CheckBox WinStartCheckBox;
        private CheckBox StartMinimizedCheckBox;
        private TabPage TempTab;
        private TreeView TreeView;
        private Button TempSelectButton;
        private TabPage AboutTab;
        private Label AboutLabel2;
        private Label label1;
        private LinkLabel linkLabel1;
    }
}