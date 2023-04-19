
namespace SM.ClubManager.AccessControl
{
    partial class frmSettings
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
            Syncfusion.Windows.Forms.Tools.ActiveStateCollection activeStateCollection1 = new Syncfusion.Windows.Forms.Tools.ActiveStateCollection();
            Syncfusion.Windows.Forms.Tools.InactiveStateCollection inactiveStateCollection1 = new Syncfusion.Windows.Forms.Tools.InactiveStateCollection();
            Syncfusion.Windows.Forms.Tools.SliderCollection sliderCollection1 = new Syncfusion.Windows.Forms.Tools.SliderCollection();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.txtSwInPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSerialInBaudrate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWifiPort = new System.Windows.Forms.TextBox();
            this.toggleisWirelessConnection = new Syncfusion.Windows.Forms.Tools.ToggleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtSerialOutPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxExt2 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.picToolTip1 = new System.Windows.Forms.PictureBox();
            this.txtInchingDelay = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.picToolTip3 = new System.Windows.Forms.PictureBox();
            this.textBoxExt3 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtSerialOutBaudRate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxExt7 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.textBoxExt6 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.textBoxExt4 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.ttOpenDelay = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.toggleisWirelessConnection)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picToolTip1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picToolTip3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt3)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt4)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSwInPort
            // 
            this.txtSwInPort.Location = new System.Drawing.Point(89, 26);
            this.txtSwInPort.Name = "txtSwInPort";
            this.txtSwInPort.Size = new System.Drawing.Size(58, 20);
            this.txtSwInPort.TabIndex = 3;
            this.txtSwInPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "COM Port";
            // 
            // txtSerialInBaudrate
            // 
            this.txtSerialInBaudrate.Location = new System.Drawing.Point(89, 52);
            this.txtSerialInBaudrate.Name = "txtSerialInBaudrate";
            this.txtSerialInBaudrate.Size = new System.Drawing.Size(58, 20);
            this.txtSerialInBaudrate.TabIndex = 5;
            this.txtSerialInBaudrate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Baudrate";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 319);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(344, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(89, 48);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(100, 20);
            this.txtIPAddress.TabIndex = 22;
            this.txtIPAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "IP Address";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Port";
            // 
            // txtWifiPort
            // 
            this.txtWifiPort.Location = new System.Drawing.Point(89, 74);
            this.txtWifiPort.Name = "txtWifiPort";
            this.txtWifiPort.Size = new System.Drawing.Size(43, 20);
            this.txtWifiPort.TabIndex = 24;
            this.txtWifiPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // toggleisWirelessConnection
            // 
            activeStateCollection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(92)))), ((int)(((byte)(97)))));
            activeStateCollection1.BorderColor = System.Drawing.Color.Silver;
            activeStateCollection1.Text = "Wi-Fi";
            this.toggleisWirelessConnection.ActiveState = activeStateCollection1;
            this.toggleisWirelessConnection.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleisWirelessConnection.ForeColor = System.Drawing.Color.Black;
            inactiveStateCollection1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(92)))), ((int)(((byte)(97)))));
            inactiveStateCollection1.BorderColor = System.Drawing.Color.Silver;
            inactiveStateCollection1.ForeColor = System.Drawing.Color.White;
            inactiveStateCollection1.Text = "USB";
            this.toggleisWirelessConnection.InactiveState = inactiveStateCollection1;
            this.toggleisWirelessConnection.Location = new System.Drawing.Point(93, 26);
            this.toggleisWirelessConnection.MinimumSize = new System.Drawing.Size(52, 20);
            this.toggleisWirelessConnection.Name = "toggleisWirelessConnection";
            this.toggleisWirelessConnection.Size = new System.Drawing.Size(90, 32);
            sliderCollection1.BackColor = System.Drawing.Color.Silver;
            sliderCollection1.BorderColor = System.Drawing.Color.Silver;
            this.toggleisWirelessConnection.Slider = sliderCollection1;
            this.toggleisWirelessConnection.TabIndex = 29;
            this.toggleisWirelessConnection.Text = "toggleButton1";
            this.toggleisWirelessConnection.ThemeStyle.BorderThickness = 1;
            this.toggleisWirelessConnection.ToggleState = Syncfusion.Windows.Forms.Tools.ToggleButtonState.Active;
            this.toggleisWirelessConnection.ToggleStateChanged += new Syncfusion.Windows.Forms.Tools.ToggleStateChangedEventHandler(this.toggleisWirelessConnection_ToggleStateChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.textBoxExt1);
            this.panel1.Location = new System.Drawing.Point(12, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 29);
            this.panel1.TabIndex = 23;
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExt1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(92)))), ((int)(((byte)(97)))));
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(192, 17);
            this.textBoxExt1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxExt1.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxExt1.ForeColor = System.Drawing.Color.White;
            this.textBoxExt1.Location = new System.Drawing.Point(3, 3);
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.Size = new System.Drawing.Size(401, 20);
            this.textBoxExt1.TabIndex = 0;
            this.textBoxExt1.Text = "System Configuration";
            this.textBoxExt1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(12, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(407, 65);
            this.panel2.TabIndex = 24;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(407, 65);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.WaitOnLoad = true;
            // 
            // txtSerialOutPort
            // 
            this.txtSerialOutPort.Location = new System.Drawing.Point(89, 133);
            this.txtSerialOutPort.Name = "txtSerialOutPort";
            this.txtSerialOutPort.Size = new System.Drawing.Size(100, 20);
            this.txtSerialOutPort.TabIndex = 22;
            this.txtSerialOutPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Port";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Wi-Fi / USB";
            // 
            // textBoxExt2
            // 
            this.textBoxExt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExt2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(92)))), ((int)(((byte)(97)))));
            this.textBoxExt2.BeforeTouchSize = new System.Drawing.Size(192, 17);
            this.textBoxExt2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxExt2.Font = new System.Drawing.Font("Calibri Light", 10F);
            this.textBoxExt2.ForeColor = System.Drawing.Color.White;
            this.textBoxExt2.Location = new System.Drawing.Point(3, 3);
            this.textBoxExt2.Name = "textBoxExt2";
            this.textBoxExt2.Size = new System.Drawing.Size(192, 17);
            this.textBoxExt2.TabIndex = 26;
            this.textBoxExt2.Text = "Global";
            this.textBoxExt2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.picToolTip1);
            this.panel3.Controls.Add(this.txtInchingDelay);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.textBoxExt2);
            this.panel3.Controls.Add(this.toggleisWirelessConnection);
            this.panel3.Location = new System.Drawing.Point(12, 119);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 95);
            this.panel3.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(135, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "sec";
            // 
            // picToolTip1
            // 
            this.picToolTip1.Image = global::SM.ClubManager.AccessControl.UI.Properties.Resources.info;
            this.picToolTip1.Location = new System.Drawing.Point(165, 66);
            this.picToolTip1.Name = "picToolTip1";
            this.picToolTip1.Size = new System.Drawing.Size(16, 16);
            this.picToolTip1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picToolTip1.TabIndex = 33;
            this.picToolTip1.TabStop = false;
            this.ttOpenDelay.SetToolTip(this.picToolTip1, "The delay in seconds BEFORE the controller activates. Min: 0, Max: 10.  Only full" +
        " seconds i.e. 1 or 2 or 3 etc.");
            // 
            // txtInchingDelay
            // 
            this.txtInchingDelay.Location = new System.Drawing.Point(93, 64);
            this.txtInchingDelay.Name = "txtInchingDelay";
            this.txtInchingDelay.Size = new System.Drawing.Size(39, 20);
            this.txtInchingDelay.TabIndex = 31;
            this.txtInchingDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Open Delay";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.picToolTip3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.txtSwInPort);
            this.panel4.Controls.Add(this.textBoxExt3);
            this.panel4.Controls.Add(this.txtSerialInBaudrate);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(12, 220);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 88);
            this.panel4.TabIndex = 31;
            // 
            // picToolTip3
            // 
            this.picToolTip3.Image = global::SM.ClubManager.AccessControl.UI.Properties.Resources.info;
            this.picToolTip3.Location = new System.Drawing.Point(163, 28);
            this.picToolTip3.Name = "picToolTip3";
            this.picToolTip3.Size = new System.Drawing.Size(16, 16);
            this.picToolTip3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picToolTip3.TabIndex = 34;
            this.picToolTip3.TabStop = false;
            this.ttOpenDelay.SetToolTip(this.picToolTip3, "RECEIVING port for Simply Switch Manager. Default value is COM9. This is NOT the " +
        "same value as the value in the Club Manager software");
            // 
            // textBoxExt3
            // 
            this.textBoxExt3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExt3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(92)))), ((int)(((byte)(97)))));
            this.textBoxExt3.BeforeTouchSize = new System.Drawing.Size(192, 17);
            this.textBoxExt3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxExt3.Font = new System.Drawing.Font("Calibri Light", 10F);
            this.textBoxExt3.ForeColor = System.Drawing.Color.White;
            this.textBoxExt3.Location = new System.Drawing.Point(2, 3);
            this.textBoxExt3.Name = "textBoxExt3";
            this.textBoxExt3.Size = new System.Drawing.Size(193, 17);
            this.textBoxExt3.TabIndex = 26;
            this.textBoxExt3.Text = "Club Manager";
            this.textBoxExt3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txtSerialOutBaudRate);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.textBoxExt7);
            this.panel5.Controls.Add(this.textBoxExt6);
            this.panel5.Controls.Add(this.txtSerialOutPort);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.textBoxExt4);
            this.panel5.Controls.Add(this.txtIPAddress);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.txtWifiPort);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(218, 119);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 189);
            this.panel5.TabIndex = 32;
            // 
            // txtSerialOutBaudRate
            // 
            this.txtSerialOutBaudRate.Location = new System.Drawing.Point(89, 159);
            this.txtSerialOutBaudRate.Name = "txtSerialOutBaudRate";
            this.txtSerialOutBaudRate.Size = new System.Drawing.Size(100, 20);
            this.txtSerialOutBaudRate.TabIndex = 27;
            this.txtSerialOutBaudRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Baudrate";
            // 
            // textBoxExt7
            // 
            this.textBoxExt7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExt7.BackColor = System.Drawing.Color.Silver;
            this.textBoxExt7.BeforeTouchSize = new System.Drawing.Size(192, 17);
            this.textBoxExt7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxExt7.Font = new System.Drawing.Font("Calibri Light", 10F);
            this.textBoxExt7.ForeColor = System.Drawing.Color.Black;
            this.textBoxExt7.Location = new System.Drawing.Point(3, 109);
            this.textBoxExt7.Name = "textBoxExt7";
            this.textBoxExt7.Size = new System.Drawing.Size(192, 17);
            this.textBoxExt7.TabIndex = 30;
            this.textBoxExt7.Text = "USB Settings";
            this.textBoxExt7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxExt6
            // 
            this.textBoxExt6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExt6.BackColor = System.Drawing.Color.Silver;
            this.textBoxExt6.BeforeTouchSize = new System.Drawing.Size(192, 17);
            this.textBoxExt6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxExt6.Font = new System.Drawing.Font("Calibri Light", 10F);
            this.textBoxExt6.ForeColor = System.Drawing.Color.Black;
            this.textBoxExt6.Location = new System.Drawing.Point(3, 26);
            this.textBoxExt6.Name = "textBoxExt6";
            this.textBoxExt6.Size = new System.Drawing.Size(192, 17);
            this.textBoxExt6.TabIndex = 29;
            this.textBoxExt6.Text = "Wi-Fi Settings";
            this.textBoxExt6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxExt4
            // 
            this.textBoxExt4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExt4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(92)))), ((int)(((byte)(97)))));
            this.textBoxExt4.BeforeTouchSize = new System.Drawing.Size(192, 17);
            this.textBoxExt4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxExt4.Font = new System.Drawing.Font("Calibri Light", 10F);
            this.textBoxExt4.ForeColor = System.Drawing.Color.White;
            this.textBoxExt4.Location = new System.Drawing.Point(3, 3);
            this.textBoxExt4.Name = "textBoxExt4";
            this.textBoxExt4.Size = new System.Drawing.Size(192, 17);
            this.textBoxExt4.TabIndex = 26;
            this.textBoxExt4.Text = "Communication";
            this.textBoxExt4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ttOpenDelay
            // 
            this.ttOpenDelay.AutomaticDelay = 100;
            this.ttOpenDelay.AutoPopDelay = 7500;
            this.ttOpenDelay.InitialDelay = 100;
            this.ttOpenDelay.IsBalloon = true;
            this.ttOpenDelay.ReshowDelay = 20;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(431, 354);
            this.ControlBox = false;
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(433, 469);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(433, 356);
            this.Name = "frmSettings";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.toggleisWirelessConnection)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picToolTip1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picToolTip3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt3)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtSwInPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerialInBaudrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWifiPort;
        private Syncfusion.Windows.Forms.Tools.ToggleButton toggleisWirelessConnection;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSerialOutPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt3;
        private System.Windows.Forms.Panel panel5;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt7;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt6;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt4;
        private System.Windows.Forms.TextBox txtSerialOutBaudRate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtInchingDelay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox picToolTip1;
        private System.Windows.Forms.PictureBox picToolTip3;
        private System.Windows.Forms.ToolTip ttOpenDelay;
        private System.Windows.Forms.Label label11;
    }
}

