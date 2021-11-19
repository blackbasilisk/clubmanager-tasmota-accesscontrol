
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lstLog = new SIS.Library.Base.Controls.ListViewExt();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.picScanResult = new System.Windows.Forms.PictureBox();
            this.textBoxExt2 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.picConnectionType = new System.Windows.Forms.PictureBox();
            this.textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.picInSerialConnection = new System.Windows.Forms.PictureBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.rtbUsbOutput = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.btnUsbCommand = new System.Windows.Forms.Button();
            this.txtUsbCommand = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.grpUsbCommandPanel = new System.Windows.Forms.GroupBox();
            this.btnUsbCommandOff = new System.Windows.Forms.Button();
            this.btnUsbCommandOn = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScanResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInSerialConnection)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsbCommand)).BeginInit();
            this.grpUsbCommandPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.Font = new System.Drawing.Font("Open Sans", 8F);
            this.lstLog.FullRowSelect = true;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLog.HideSelection = false;
            this.lstLog.IncludeDateTime = true;
            this.lstLog.IsCloneListEntriesEnabled = false;
            this.lstLog.IsClosing = false;
            this.lstLog.IsDebugMode = false;
            this.lstLog.Location = new System.Drawing.Point(6, 19);
            this.lstLog.MaxCharactersPerLine = 0;
            this.lstLog.MaxEntries = 500;
            this.lstLog.Name = "lstLog";
            this.lstLog.SelectedItemBackgroundColor = System.Drawing.Color.White;
            this.lstLog.SelectedItemForegroundColor = System.Drawing.Color.AliceBlue;
            this.lstLog.Size = new System.Drawing.Size(357, 468);
            this.lstLog.TabIndex = 99;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.picScanResult);
            this.groupBox3.Controls.Add(this.textBoxExt2);
            this.groupBox3.Controls.Add(this.picConnectionType);
            this.groupBox3.Controls.Add(this.textBoxExt1);
            this.groupBox3.Controls.Add(this.picInSerialConnection);
            this.groupBox3.Location = new System.Drawing.Point(12, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(206, 112);
            this.groupBox3.TabIndex = 99;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Main";
            // 
            // picScanResult
            // 
            this.picScanResult.Image = global::SM.ClubManager.AccessControl.Properties.Resources.access_granted;
            this.picScanResult.Location = new System.Drawing.Point(160, 18);
            this.picScanResult.Name = "picScanResult";
            this.picScanResult.Size = new System.Drawing.Size(33, 33);
            this.picScanResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picScanResult.TabIndex = 100;
            this.picScanResult.TabStop = false;
            this.picScanResult.Visible = false;
            // 
            // textBoxExt2
            // 
            this.textBoxExt2.BackColor = System.Drawing.Color.White;
            this.textBoxExt2.BeforeTouchSize = new System.Drawing.Size(193, 20);
            this.textBoxExt2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxExt2.DrawActiveWhenDisabled = true;
            this.textBoxExt2.Enabled = false;
            this.textBoxExt2.Font = new System.Drawing.Font("Open Sans", 10F);
            this.textBoxExt2.Location = new System.Drawing.Point(52, 68);
            this.textBoxExt2.Multiline = true;
            this.textBoxExt2.Name = "textBoxExt2";
            this.textBoxExt2.Size = new System.Drawing.Size(141, 23);
            this.textBoxExt2.TabIndex = 99;
            this.textBoxExt2.Text = "Device Connection";
            // 
            // picConnectionType
            // 
            this.picConnectionType.Image = global::SM.ClubManager.AccessControl.Properties.Resources.Unchecked;
            this.picConnectionType.Location = new System.Drawing.Point(13, 63);
            this.picConnectionType.Name = "picConnectionType";
            this.picConnectionType.Size = new System.Drawing.Size(33, 33);
            this.picConnectionType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picConnectionType.TabIndex = 18;
            this.picConnectionType.TabStop = false;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BackColor = System.Drawing.Color.White;
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(193, 20);
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxExt1.DrawActiveWhenDisabled = true;
            this.textBoxExt1.Enabled = false;
            this.textBoxExt1.Font = new System.Drawing.Font("Open Sans", 10F);
            this.textBoxExt1.Location = new System.Drawing.Point(52, 18);
            this.textBoxExt1.Multiline = true;
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.Size = new System.Drawing.Size(148, 33);
            this.textBoxExt1.TabIndex = 99;
            this.textBoxExt1.Text = "ClubManager Connection";
            // 
            // picInSerialConnection
            // 
            this.picInSerialConnection.Image = global::SM.ClubManager.AccessControl.Properties.Resources.Unchecked;
            this.picInSerialConnection.Location = new System.Drawing.Point(13, 18);
            this.picInSerialConnection.Name = "picInSerialConnection";
            this.picInSerialConnection.Size = new System.Drawing.Size(33, 33);
            this.picInSerialConnection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picInSerialConnection.TabIndex = 16;
            this.picInSerialConnection.TabStop = false;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 201);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // rtbUsbOutput
            // 
            this.rtbUsbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbUsbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbUsbOutput.DetectUrls = false;
            this.rtbUsbOutput.Font = new System.Drawing.Font("DejaVu Sans", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbUsbOutput.Location = new System.Drawing.Point(6, 19);
            this.rtbUsbOutput.Name = "rtbUsbOutput";
            this.rtbUsbOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbUsbOutput.ShortcutsEnabled = false;
            this.rtbUsbOutput.Size = new System.Drawing.Size(385, 467);
            this.rtbUsbOutput.TabIndex = 99;
            this.rtbUsbOutput.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstLog);
            this.groupBox2.Location = new System.Drawing.Point(225, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 493);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Application Events";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rtbUsbOutput);
            this.groupBox5.Location = new System.Drawing.Point(601, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(397, 492);
            this.groupBox5.TabIndex = 99;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "USB Events";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnViewLogs
            // 
            this.btnViewLogs.Location = new System.Drawing.Point(144, 201);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(75, 23);
            this.btnViewLogs.TabIndex = 1;
            this.btnViewLogs.Text = "Toggle Logs";
            this.btnViewLogs.UseVisualStyleBackColor = true;
            this.btnViewLogs.Click += new System.EventHandler(this.btnViewLogs_Click);
            // 
            // btnUsbCommand
            // 
            this.btnUsbCommand.Location = new System.Drawing.Point(6, 45);
            this.btnUsbCommand.Name = "btnUsbCommand";
            this.btnUsbCommand.Size = new System.Drawing.Size(75, 23);
            this.btnUsbCommand.TabIndex = 4;
            this.btnUsbCommand.Text = "Send";
            this.btnUsbCommand.UseVisualStyleBackColor = true;
            this.btnUsbCommand.Click += new System.EventHandler(this.btnUsbCommand_Click);
            // 
            // txtUsbCommand
            // 
            this.txtUsbCommand.BeforeTouchSize = new System.Drawing.Size(193, 20);
            this.txtUsbCommand.Location = new System.Drawing.Point(6, 19);
            this.txtUsbCommand.Name = "txtUsbCommand";
            this.txtUsbCommand.Size = new System.Drawing.Size(193, 20);
            this.txtUsbCommand.TabIndex = 3;
            // 
            // grpUsbCommandPanel
            // 
            this.grpUsbCommandPanel.Controls.Add(this.btnUsbCommandOff);
            this.grpUsbCommandPanel.Controls.Add(this.btnUsbCommandOn);
            this.grpUsbCommandPanel.Controls.Add(this.txtUsbCommand);
            this.grpUsbCommandPanel.Controls.Add(this.btnUsbCommand);
            this.grpUsbCommandPanel.Enabled = false;
            this.grpUsbCommandPanel.Location = new System.Drawing.Point(13, 398);
            this.grpUsbCommandPanel.Name = "grpUsbCommandPanel";
            this.grpUsbCommandPanel.Size = new System.Drawing.Size(206, 107);
            this.grpUsbCommandPanel.TabIndex = 99;
            this.grpUsbCommandPanel.TabStop = false;
            this.grpUsbCommandPanel.Text = "USB Commands";
            // 
            // btnUsbCommandOff
            // 
            this.btnUsbCommandOff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUsbCommandOff.Location = new System.Drawing.Point(53, 78);
            this.btnUsbCommandOff.Name = "btnUsbCommandOff";
            this.btnUsbCommandOff.Size = new System.Drawing.Size(37, 23);
            this.btnUsbCommandOff.TabIndex = 6;
            this.btnUsbCommandOff.Text = "OFF";
            this.btnUsbCommandOff.UseVisualStyleBackColor = true;
            this.btnUsbCommandOff.Click += new System.EventHandler(this.btnUsbCommandOff_Click);
            // 
            // btnUsbCommandOn
            // 
            this.btnUsbCommandOn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUsbCommandOn.Location = new System.Drawing.Point(6, 78);
            this.btnUsbCommandOn.Name = "btnUsbCommandOn";
            this.btnUsbCommandOn.Size = new System.Drawing.Size(40, 23);
            this.btnUsbCommandOn.TabIndex = 5;
            this.btnUsbCommandOn.Text = "ON";
            this.btnUsbCommandOn.UseVisualStyleBackColor = true;
            this.btnUsbCommandOn.Click += new System.EventHandler(this.btnUsbCommandOn_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(13, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(206, 65);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnUsbCommand;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 517);
            this.Controls.Add(this.grpUsbCommandPanel);
            this.Controls.Add(this.btnViewLogs);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 556);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScanResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConnectionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInSerialConnection)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUsbCommand)).EndInit();
            this.grpUsbCommandPanel.ResumeLayout(false);
            this.grpUsbCommandPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private SIS.Library.Base.Controls.ListViewExt lstLog;
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
    }
}

