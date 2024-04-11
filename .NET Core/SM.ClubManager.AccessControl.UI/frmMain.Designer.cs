
using Syncfusion.XPS;
using WinRT;

namespace SM.ClubManager.AccessControl
{
    partial class frmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            lstLog = new SIS.Library.Base.Controls.ListViewExt();
            groupBox3 = new GroupBox();
            picScanResult = new PictureBox();
            textBoxExt2 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            picConnectionType = new PictureBox();
            textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            picInSerialConnection = new PictureBox();
            btnSettings = new Button();
            rtbUsbOutput = new RichTextBox();
            groupBox2 = new GroupBox();
            groupBox5 = new GroupBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            btnViewLogs = new Button();
            btnUsbCommand = new Button();
            txtUsbCommand = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            grpUsbCommandPanel = new GroupBox();
            btnRestart = new Button();
            btnUsbCommandOff = new Button();
            btnUsbCommandOn = new Button();
            pictureBox2 = new PictureBox();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picScanResult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picConnectionType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInSerialConnection).BeginInit();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtUsbCommand).BeginInit();
            grpUsbCommandPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // lstLog
            // 
            lstLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstLog.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lstLog.FullRowSelect = true;
            lstLog.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lstLog.IncludeDateTime = true;
            lstLog.IsCloneListEntriesEnabled = false;
            lstLog.IsClosing = false;
            lstLog.IsDebugMode = false;
            lstLog.Location = new Point(7, 22);
            lstLog.Margin = new Padding(4, 3, 4, 3);
            lstLog.MaxCharactersPerLine = 0;
            lstLog.MaxEntries = 500;
            lstLog.Name = "lstLog";
            lstLog.SelectedItemBackgroundColor = Color.White;
            lstLog.SelectedItemForegroundColor = Color.AliceBlue;
            lstLog.Size = new Size(416, 539);
            lstLog.TabIndex = 99;
            lstLog.UseCompatibleStateImageBehavior = false;
            lstLog.View = View.Details;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(picScanResult);
            groupBox3.Controls.Add(textBoxExt2);
            groupBox3.Controls.Add(picConnectionType);
            groupBox3.Controls.Add(textBoxExt1);
            groupBox3.Controls.Add(picInSerialConnection);
            groupBox3.Location = new Point(14, 96);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(240, 129);
            groupBox3.TabIndex = 99;
            groupBox3.TabStop = false;
            groupBox3.Text = "Main";
            // 
            // picScanResult
            // 
            picScanResult.Image = UI.Properties.Resources.access_granted;
            picScanResult.Location = new Point(187, 21);
            picScanResult.Margin = new Padding(4, 3, 4, 3);
            picScanResult.Name = "picScanResult";
            picScanResult.Size = new Size(38, 38);
            picScanResult.SizeMode = PictureBoxSizeMode.Zoom;
            picScanResult.TabIndex = 100;
            picScanResult.TabStop = false;
            picScanResult.Visible = false;
            // 
            // textBoxExt2
            // 
            textBoxExt2.BackColor = Color.White;
            textBoxExt2.BeforeTouchSize = new Size(224, 23);
            textBoxExt2.BorderStyle = BorderStyle.None;
            textBoxExt2.DrawActiveWhenDisabled = true;
            textBoxExt2.Enabled = false;
            textBoxExt2.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt2.Location = new Point(61, 78);
            textBoxExt2.Margin = new Padding(4, 3, 4, 3);
            textBoxExt2.Multiline = true;
            textBoxExt2.Name = "textBoxExt2";
            textBoxExt2.Size = new Size(129, 27);
            textBoxExt2.TabIndex = 99;
            textBoxExt2.Text = "Device Connection";
            // 
            // picConnectionType
            // 
            picConnectionType.Image = UI.Properties.Resources._unchecked;
            picConnectionType.Location = new Point(15, 73);
            picConnectionType.Margin = new Padding(4, 3, 4, 3);
            picConnectionType.Name = "picConnectionType";
            picConnectionType.Size = new Size(38, 38);
            picConnectionType.SizeMode = PictureBoxSizeMode.Zoom;
            picConnectionType.TabIndex = 18;
            picConnectionType.TabStop = false;
            // 
            // textBoxExt1
            // 
            textBoxExt1.BackColor = Color.White;
            textBoxExt1.BeforeTouchSize = new Size(224, 23);
            textBoxExt1.BorderStyle = BorderStyle.None;
            textBoxExt1.DrawActiveWhenDisabled = true;
            textBoxExt1.Enabled = false;
            textBoxExt1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt1.Location = new Point(61, 21);
            textBoxExt1.Margin = new Padding(4, 3, 4, 3);
            textBoxExt1.Multiline = true;
            textBoxExt1.Name = "textBoxExt1";
            textBoxExt1.Size = new Size(173, 38);
            textBoxExt1.TabIndex = 99;
            textBoxExt1.Text = "ClubManager Connection";
            // 
            // picInSerialConnection
            // 
            picInSerialConnection.Image = UI.Properties.Resources._unchecked;
            picInSerialConnection.Location = new Point(15, 21);
            picInSerialConnection.Margin = new Padding(4, 3, 4, 3);
            picInSerialConnection.Name = "picInSerialConnection";
            picInSerialConnection.Size = new Size(38, 38);
            picInSerialConnection.SizeMode = PictureBoxSizeMode.Zoom;
            picInSerialConnection.TabIndex = 16;
            picInSerialConnection.TabStop = false;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(14, 232);
            btnSettings.Margin = new Padding(4, 3, 4, 3);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(88, 27);
            btnSettings.TabIndex = 0;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // rtbUsbOutput
            // 
            rtbUsbOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbUsbOutput.BorderStyle = BorderStyle.FixedSingle;
            rtbUsbOutput.DetectUrls = false;
            rtbUsbOutput.Font = new Font("Microsoft Sans Serif", 6.75F, FontStyle.Regular, GraphicsUnit.Point);
            rtbUsbOutput.Location = new Point(7, 22);
            rtbUsbOutput.Margin = new Padding(4, 3, 4, 3);
            rtbUsbOutput.Name = "rtbUsbOutput";
            rtbUsbOutput.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbUsbOutput.ShortcutsEnabled = false;
            rtbUsbOutput.Size = new Size(448, 538);
            rtbUsbOutput.TabIndex = 99;
            rtbUsbOutput.Text = "";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lstLog);
            groupBox2.Location = new Point(262, 14);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(430, 569);
            groupBox2.TabIndex = 99;
            groupBox2.TabStop = false;
            groupBox2.Text = "Application Events";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(rtbUsbOutput);
            groupBox5.Location = new Point(701, 15);
            groupBox5.Margin = new Padding(4, 3, 4, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4, 3, 4, 3);
            groupBox5.Size = new Size(463, 568);
            groupBox5.TabIndex = 99;
            groupBox5.TabStop = false;
            groupBox5.Text = "USB Events";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // btnViewLogs
            // 
            btnViewLogs.Location = new Point(168, 232);
            btnViewLogs.Margin = new Padding(4, 3, 4, 3);
            btnViewLogs.Name = "btnViewLogs";
            btnViewLogs.Size = new Size(88, 27);
            btnViewLogs.TabIndex = 1;
            btnViewLogs.Text = "Toggle Logs";
            btnViewLogs.UseVisualStyleBackColor = true;
            btnViewLogs.Click += btnViewLogs_Click;
            // 
            // btnUsbCommand
            // 
            btnUsbCommand.Location = new Point(7, 52);
            btnUsbCommand.Margin = new Padding(4, 3, 4, 3);
            btnUsbCommand.Name = "btnUsbCommand";
            btnUsbCommand.Size = new Size(88, 27);
            btnUsbCommand.TabIndex = 4;
            btnUsbCommand.Text = "Send";
            btnUsbCommand.UseVisualStyleBackColor = true;
            btnUsbCommand.Click += btnUsbCommand_Click;
            // 
            // txtUsbCommand
            // 
            txtUsbCommand.BeforeTouchSize = new Size(224, 23);
            txtUsbCommand.Location = new Point(7, 22);
            txtUsbCommand.Margin = new Padding(4, 3, 4, 3);
            txtUsbCommand.Name = "txtUsbCommand";
            txtUsbCommand.Size = new Size(224, 23);
            txtUsbCommand.TabIndex = 3;
            // 
            // grpUsbCommandPanel
            // 
            grpUsbCommandPanel.Controls.Add(btnRestart);
            grpUsbCommandPanel.Controls.Add(btnUsbCommandOff);
            grpUsbCommandPanel.Controls.Add(btnUsbCommandOn);
            grpUsbCommandPanel.Controls.Add(txtUsbCommand);
            grpUsbCommandPanel.Controls.Add(btnUsbCommand);
            grpUsbCommandPanel.Enabled = false;
            grpUsbCommandPanel.Location = new Point(15, 459);
            grpUsbCommandPanel.Margin = new Padding(4, 3, 4, 3);
            grpUsbCommandPanel.Name = "grpUsbCommandPanel";
            grpUsbCommandPanel.Padding = new Padding(4, 3, 4, 3);
            grpUsbCommandPanel.Size = new Size(240, 123);
            grpUsbCommandPanel.TabIndex = 99;
            grpUsbCommandPanel.TabStop = false;
            grpUsbCommandPanel.Text = "USB Commands";
            // 
            // btnRestart
            // 
            btnRestart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnRestart.Location = new Point(113, 90);
            btnRestart.Margin = new Padding(4, 3, 4, 3);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(86, 27);
            btnRestart.TabIndex = 7;
            btnRestart.Text = "RESTART";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // btnUsbCommandOff
            // 
            btnUsbCommandOff.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnUsbCommandOff.Location = new Point(62, 90);
            btnUsbCommandOff.Margin = new Padding(4, 3, 4, 3);
            btnUsbCommandOff.Name = "btnUsbCommandOff";
            btnUsbCommandOff.Size = new Size(43, 27);
            btnUsbCommandOff.TabIndex = 6;
            btnUsbCommandOff.Text = "OFF";
            btnUsbCommandOff.UseVisualStyleBackColor = true;
            btnUsbCommandOff.Click += btnUsbCommandOff_Click;
            // 
            // btnUsbCommandOn
            // 
            btnUsbCommandOn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnUsbCommandOn.Location = new Point(7, 90);
            btnUsbCommandOn.Margin = new Padding(4, 3, 4, 3);
            btnUsbCommandOn.Name = "btnUsbCommandOn";
            btnUsbCommandOn.Size = new Size(47, 27);
            btnUsbCommandOn.TabIndex = 5;
            btnUsbCommandOn.Text = "ON";
            btnUsbCommandOn.UseVisualStyleBackColor = true;
            btnUsbCommandOn.Click += btnUsbCommandOn_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.ErrorImage = null;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(15, 14);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(240, 75);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 15;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            pictureBox2.DoubleClick += pictureBox2_DoubleClick;
            // 
            // frmMain
            // 
            AcceptButton = btnUsbCommand;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(700, 591);
            Controls.Add(grpUsbCommandPanel);
            Controls.Add(btnViewLogs);
            Controls.Add(groupBox5);
            Controls.Add(groupBox2);
            Controls.Add(btnSettings);
            Controls.Add(pictureBox2);
            Controls.Add(groupBox3);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new Size(1192, 636);
            Name = "frmMain";
            SizeGripStyle = SizeGripStyle.Hide;
            FormClosing += frmMain_FormClosing;
            FormClosed += frmMain_FormClosed;
            Load += frmMain_Load;
            Shown += frmMain_Shown;
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picScanResult).EndInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picConnectionType).EndInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInSerialConnection).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtUsbCommand).EndInit();
            grpUsbCommandPanel.ResumeLayout(false);
            grpUsbCommandPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox picInSerialConnection;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private System.Windows.Forms.PictureBox picConnectionType;
        private System.Windows.Forms.RichTextBox rtbUsbOutput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt2;
        private System.Windows.Forms.Button btnViewLogs;
        private System.Windows.Forms.Button btnUsbCommand;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtUsbCommand;
        private System.Windows.Forms.GroupBox grpUsbCommandPanel;
        private System.Windows.Forms.Button btnUsbCommandOff;
        private System.Windows.Forms.Button btnUsbCommandOn;
        private System.Windows.Forms.PictureBox picScanResult;
        private SIS.Library.Base.Controls.ListViewExt lstLog;
        private Button btnRestart;
    }
}

