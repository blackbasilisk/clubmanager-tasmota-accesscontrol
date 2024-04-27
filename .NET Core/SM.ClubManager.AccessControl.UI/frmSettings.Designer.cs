
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
            components = new System.ComponentModel.Container();
            Syncfusion.Windows.Forms.Tools.ActiveStateCollection activeStateCollection1 = new Syncfusion.Windows.Forms.Tools.ActiveStateCollection();
            Syncfusion.Windows.Forms.Tools.InactiveStateCollection inactiveStateCollection1 = new Syncfusion.Windows.Forms.Tools.InactiveStateCollection();
            Syncfusion.Windows.Forms.Tools.SliderCollection sliderCollection1 = new Syncfusion.Windows.Forms.Tools.SliderCollection();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            txtPort2 = new TextBox();
            label1 = new Label();
            txtSerialPortPairBaudrate = new TextBox();
            label2 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            txtIPAddress = new TextBox();
            label4 = new Label();
            label5 = new Label();
            txtWifiPort = new TextBox();
            toggleisWirelessConnection = new Syncfusion.Windows.Forms.Tools.ToggleButton();
            panel1 = new Panel();
            textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            txtSerialPortSimplySwitchName = new TextBox();
            label3 = new Label();
            label9 = new Label();
            textBoxExt2 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            panel3 = new Panel();
            label11 = new Label();
            picToolTip1 = new PictureBox();
            txtInchingDelay = new TextBox();
            label8 = new Label();
            panel4 = new Panel();
            textBoxExt6 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            picToolTip3 = new PictureBox();
            textBoxExt3 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            panel5 = new Panel();
            lblSSPortDetectStatus = new Label();
            label12 = new Label();
            txtPort1 = new TextBox();
            txtSerialOutBaudRate = new TextBox();
            label10 = new Label();
            textBoxExt7 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            textBoxExt4 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            ttOpenDelay = new ToolTip(components);
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            chkAutoConfigSSPort = new CheckBox();
            btnAutoSetupSS = new Button();
            btnAutoSetupVSPE = new Button();
            panel6 = new Panel();
            label7 = new Label();
            txtVSPEConfigPath = new TextBox();
            btnBrowsePathVSPE = new Button();
            label6 = new Label();
            textBoxExt5 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            btnGenerateVSPEConfig = new Button();
            button1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)toggleisWirelessConnection).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxExt1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt2).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picToolTip1).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxExt6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picToolTip3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt3).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxExt7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxExt5).BeginInit();
            SuspendLayout();
            // 
            // txtPort2
            // 
            txtPort2.Location = new Point(73, 83);
            txtPort2.Margin = new Padding(4, 3, 4, 3);
            txtPort2.Name = "txtPort2";
            txtPort2.Size = new Size(67, 23);
            txtPort2.TabIndex = 3;
            txtPort2.KeyPress += textBox_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 59);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "Port 1";
            // 
            // txtSerialPortPairBaudrate
            // 
            txtSerialPortPairBaudrate.Location = new Point(73, 112);
            txtSerialPortPairBaudrate.Margin = new Padding(4, 3, 4, 3);
            txtSerialPortPairBaudrate.Name = "txtSerialPortPairBaudrate";
            txtSerialPortPairBaudrate.Size = new Size(67, 23);
            txtSerialPortPairBaudrate.TabIndex = 5;
            txtSerialPortPairBaudrate.KeyPress += textBox_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 117);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 6;
            label2.Text = "Baudrate";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(13, 422);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 20;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(636, 422);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtIPAddress
            // 
            txtIPAddress.Location = new Point(111, 36);
            txtIPAddress.Margin = new Padding(4, 3, 4, 3);
            txtIPAddress.Name = "txtIPAddress";
            txtIPAddress.Size = new Size(116, 23);
            txtIPAddress.TabIndex = 22;
            txtIPAddress.KeyPress += textBox_KeyPress;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 39);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 23;
            label4.Text = "IP Address";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(4, 69);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(29, 15);
            label5.TabIndex = 25;
            label5.Text = "Port";
            // 
            // txtWifiPort
            // 
            txtWifiPort.Location = new Point(111, 66);
            txtWifiPort.Margin = new Padding(4, 3, 4, 3);
            txtWifiPort.Name = "txtWifiPort";
            txtWifiPort.Size = new Size(50, 23);
            txtWifiPort.TabIndex = 24;
            txtWifiPort.KeyPress += textBox_KeyPress;
            // 
            // toggleisWirelessConnection
            // 
            activeStateCollection1.BackColor = Color.FromArgb(93, 92, 97);
            activeStateCollection1.BorderColor = Color.Silver;
            activeStateCollection1.Text = "Wi-Fi";
            toggleisWirelessConnection.ActiveState = activeStateCollection1;
            toggleisWirelessConnection.Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toggleisWirelessConnection.ForeColor = Color.Black;
            inactiveStateCollection1.BackColor = Color.FromArgb(93, 92, 97);
            inactiveStateCollection1.BorderColor = Color.Silver;
            inactiveStateCollection1.ForeColor = Color.White;
            inactiveStateCollection1.Text = "USB";
            toggleisWirelessConnection.InactiveState = inactiveStateCollection1;
            toggleisWirelessConnection.Location = new Point(122, 31);
            toggleisWirelessConnection.Margin = new Padding(4, 3, 4, 3);
            toggleisWirelessConnection.MinimumSize = new Size(61, 23);
            toggleisWirelessConnection.Name = "toggleisWirelessConnection";
            toggleisWirelessConnection.Size = new Size(105, 37);
            sliderCollection1.BackColor = Color.Silver;
            sliderCollection1.BorderColor = Color.Silver;
            toggleisWirelessConnection.Slider = sliderCollection1;
            toggleisWirelessConnection.TabIndex = 29;
            toggleisWirelessConnection.Text = "toggleButton1";
            toggleisWirelessConnection.ThemeStyle.BorderThickness = 1;
            toggleisWirelessConnection.ToggleStateChanged += toggleisWirelessConnection_ToggleStateChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(textBoxExt1);
            panel1.Location = new Point(14, 97);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(712, 33);
            panel1.TabIndex = 23;
            // 
            // textBoxExt1
            // 
            textBoxExt1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxExt1.BackColor = Color.FromArgb(93, 92, 97);
            textBoxExt1.BeforeTouchSize = new Size(227, 17);
            textBoxExt1.BorderStyle = BorderStyle.None;
            textBoxExt1.Font = new Font("Calibri Light", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt1.ForeColor = Color.White;
            textBoxExt1.Location = new Point(4, 3);
            textBoxExt1.Margin = new Padding(4, 3, 4, 3);
            textBoxExt1.Name = "textBoxExt1";
            textBoxExt1.Size = new Size(705, 20);
            textBoxExt1.TabIndex = 0;
            textBoxExt1.Text = "System Configuration";
            textBoxExt1.TextAlign = HorizontalAlignment.Center;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(pictureBox2);
            panel2.Location = new Point(14, 15);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(712, 75);
            panel2.TabIndex = 24;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.ErrorImage = null;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Margin = new Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(712, 75);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 22;
            pictureBox2.TabStop = false;
            pictureBox2.WaitOnLoad = true;
            // 
            // txtSerialPortSimplySwitchName
            // 
            txtSerialPortSimplySwitchName.Location = new Point(73, 182);
            txtSerialPortSimplySwitchName.Margin = new Padding(4, 3, 4, 3);
            txtSerialPortSimplySwitchName.Name = "txtSerialPortSimplySwitchName";
            txtSerialPortSimplySwitchName.Size = new Size(67, 23);
            txtSerialPortSimplySwitchName.TabIndex = 22;
            txtSerialPortSimplySwitchName.TextChanged += txtSerialPortSimplySwitchName_TextChanged;
            txtSerialPortSimplySwitchName.KeyPress += textBox_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 186);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 23;
            label3.Text = "Port";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(4, 40);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(67, 15);
            label9.TabIndex = 30;
            label9.Text = "Wi-Fi / USB";
            // 
            // textBoxExt2
            // 
            textBoxExt2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxExt2.BackColor = Color.FromArgb(93, 92, 97);
            textBoxExt2.BeforeTouchSize = new Size(227, 17);
            textBoxExt2.BorderStyle = BorderStyle.None;
            textBoxExt2.Font = new Font("Calibri Light", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt2.ForeColor = Color.White;
            textBoxExt2.Location = new Point(4, 3);
            textBoxExt2.Margin = new Padding(4, 3, 4, 3);
            textBoxExt2.Name = "textBoxExt2";
            textBoxExt2.Size = new Size(224, 17);
            textBoxExt2.TabIndex = 26;
            textBoxExt2.Text = "Global";
            textBoxExt2.TextAlign = HorizontalAlignment.Center;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(label11);
            panel3.Controls.Add(picToolTip1);
            panel3.Controls.Add(txtInchingDelay);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(textBoxExt2);
            panel3.Controls.Add(toggleisWirelessConnection);
            panel3.Location = new Point(251, 136);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(233, 109);
            panel3.TabIndex = 27;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(158, 82);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(24, 15);
            label11.TabIndex = 35;
            label11.Text = "sec";
            // 
            // picToolTip1
            // 
            picToolTip1.Image = UI.Properties.Resources.info;
            picToolTip1.Location = new Point(208, 77);
            picToolTip1.Margin = new Padding(4, 3, 4, 3);
            picToolTip1.Name = "picToolTip1";
            picToolTip1.Size = new Size(19, 18);
            picToolTip1.SizeMode = PictureBoxSizeMode.Zoom;
            picToolTip1.TabIndex = 33;
            picToolTip1.TabStop = false;
            ttOpenDelay.SetToolTip(picToolTip1, "The delay in seconds BEFORE the controller activates. Min: 0, Max: 10.  Only full seconds i.e. 1 or 2 or 3 etc.");
            // 
            // txtInchingDelay
            // 
            txtInchingDelay.Location = new Point(108, 74);
            txtInchingDelay.Margin = new Padding(4, 3, 4, 3);
            txtInchingDelay.Name = "txtInchingDelay";
            txtInchingDelay.Size = new Size(45, 23);
            txtInchingDelay.TabIndex = 31;
            txtInchingDelay.KeyPress += textBox_KeyPress;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 77);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(68, 15);
            label8.TabIndex = 32;
            label8.Text = "Open Delay";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(textBoxExt6);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(txtWifiPort);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(txtIPAddress);
            panel4.Location = new Point(491, 137);
            panel4.Margin = new Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(233, 108);
            panel4.TabIndex = 31;
            // 
            // textBoxExt6
            // 
            textBoxExt6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxExt6.BackColor = Color.FromArgb(93, 92, 97);
            textBoxExt6.BeforeTouchSize = new Size(227, 17);
            textBoxExt6.BorderStyle = BorderStyle.None;
            textBoxExt6.Font = new Font("Calibri Light", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt6.ForeColor = Color.White;
            textBoxExt6.Location = new Point(3, 3);
            textBoxExt6.Margin = new Padding(4, 3, 4, 3);
            textBoxExt6.Name = "textBoxExt6";
            textBoxExt6.Size = new Size(224, 17);
            textBoxExt6.TabIndex = 29;
            textBoxExt6.Text = "Wi-Fi Settings";
            textBoxExt6.TextAlign = HorizontalAlignment.Center;
            // 
            // picToolTip3
            // 
            picToolTip3.Image = UI.Properties.Resources.info;
            picToolTip3.Location = new Point(196, 55);
            picToolTip3.Margin = new Padding(4, 3, 4, 3);
            picToolTip3.Name = "picToolTip3";
            picToolTip3.Size = new Size(19, 18);
            picToolTip3.SizeMode = PictureBoxSizeMode.Zoom;
            picToolTip3.TabIndex = 34;
            picToolTip3.TabStop = false;
            ttOpenDelay.SetToolTip(picToolTip3, "RECEIVING port for Simply Switch Manager. Default value is COM9. This is NOT the same value as the value in the Club Manager software");
            picToolTip3.Click += picToolTip3_Click;
            // 
            // textBoxExt3
            // 
            textBoxExt3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxExt3.BackColor = Color.Silver;
            textBoxExt3.BeforeTouchSize = new Size(227, 17);
            textBoxExt3.BorderStyle = BorderStyle.None;
            textBoxExt3.Font = new Font("Calibri Light", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt3.ForeColor = Color.Black;
            textBoxExt3.Location = new Point(4, 26);
            textBoxExt3.Margin = new Padding(4, 3, 4, 3);
            textBoxExt3.Name = "textBoxExt3";
            textBoxExt3.Size = new Size(225, 17);
            textBoxExt3.TabIndex = 26;
            textBoxExt3.Text = "Club Manager";
            textBoxExt3.TextAlign = HorizontalAlignment.Center;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(lblSSPortDetectStatus);
            panel5.Controls.Add(label12);
            panel5.Controls.Add(txtPort1);
            panel5.Controls.Add(picToolTip3);
            panel5.Controls.Add(txtSerialOutBaudRate);
            panel5.Controls.Add(label1);
            panel5.Controls.Add(label10);
            panel5.Controls.Add(txtPort2);
            panel5.Controls.Add(textBoxExt7);
            panel5.Controls.Add(textBoxExt3);
            panel5.Controls.Add(txtSerialPortPairBaudrate);
            panel5.Controls.Add(txtSerialPortSimplySwitchName);
            panel5.Controls.Add(label2);
            panel5.Controls.Add(label3);
            panel5.Controls.Add(textBoxExt4);
            panel5.Location = new Point(14, 136);
            panel5.Margin = new Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(233, 266);
            panel5.TabIndex = 32;
            // 
            // lblSSPortDetectStatus
            // 
            lblSSPortDetectStatus.AutoSize = true;
            lblSSPortDetectStatus.Location = new Point(148, 187);
            lblSSPortDetectStatus.Margin = new Padding(4, 0, 4, 0);
            lblSSPortDetectStatus.Name = "lblSSPortDetectStatus";
            lblSSPortDetectStatus.Size = new Size(0, 15);
            lblSSPortDetectStatus.TabIndex = 43;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(11, 88);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(38, 15);
            label12.TabIndex = 36;
            label12.Text = "Port 2";
            // 
            // txtPort1
            // 
            txtPort1.Location = new Point(73, 54);
            txtPort1.Margin = new Padding(4, 3, 4, 3);
            txtPort1.Name = "txtPort1";
            txtPort1.Size = new Size(67, 23);
            txtPort1.TabIndex = 35;
            // 
            // txtSerialOutBaudRate
            // 
            txtSerialOutBaudRate.Location = new Point(73, 212);
            txtSerialOutBaudRate.Margin = new Padding(4, 3, 4, 3);
            txtSerialOutBaudRate.Name = "txtSerialOutBaudRate";
            txtSerialOutBaudRate.Size = new Size(67, 23);
            txtSerialOutBaudRate.TabIndex = 27;
            txtSerialOutBaudRate.KeyPress += textBox_KeyPress;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(8, 216);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(54, 15);
            label10.TabIndex = 28;
            label10.Text = "Baudrate";
            // 
            // textBoxExt7
            // 
            textBoxExt7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxExt7.BackColor = Color.Silver;
            textBoxExt7.BeforeTouchSize = new Size(227, 17);
            textBoxExt7.BorderStyle = BorderStyle.None;
            textBoxExt7.Font = new Font("Calibri Light", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt7.ForeColor = Color.Black;
            textBoxExt7.Location = new Point(3, 156);
            textBoxExt7.Margin = new Padding(4, 3, 4, 3);
            textBoxExt7.Name = "textBoxExt7";
            textBoxExt7.Size = new Size(224, 17);
            textBoxExt7.TabIndex = 30;
            textBoxExt7.Text = "Simply Switch";
            textBoxExt7.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxExt4
            // 
            textBoxExt4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxExt4.BackColor = Color.FromArgb(93, 92, 97);
            textBoxExt4.BeforeTouchSize = new Size(227, 17);
            textBoxExt4.BorderStyle = BorderStyle.None;
            textBoxExt4.Font = new Font("Calibri Light", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt4.ForeColor = Color.White;
            textBoxExt4.Location = new Point(4, 3);
            textBoxExt4.Margin = new Padding(4, 3, 4, 3);
            textBoxExt4.Name = "textBoxExt4";
            textBoxExt4.Size = new Size(224, 17);
            textBoxExt4.TabIndex = 26;
            textBoxExt4.Text = "Communication";
            textBoxExt4.TextAlign = HorizontalAlignment.Center;
            // 
            // ttOpenDelay
            // 
            ttOpenDelay.AutomaticDelay = 100;
            ttOpenDelay.AutoPopDelay = 7500;
            ttOpenDelay.InitialDelay = 100;
            ttOpenDelay.IsBalloon = true;
            ttOpenDelay.ReshowDelay = 20;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = UI.Properties.Resources.info;
            pictureBox1.Location = new Point(208, 38);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(19, 18);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 36;
            pictureBox1.TabStop = false;
            ttOpenDelay.SetToolTip(pictureBox1, "Enable/Disable AUTO detection of Simply Switch port when starting up");
            // 
            // pictureBox3
            // 
            pictureBox3.Image = UI.Properties.Resources.info;
            pictureBox3.Location = new Point(208, 68);
            pictureBox3.Margin = new Padding(4, 3, 4, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(19, 18);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 39;
            pictureBox3.TabStop = false;
            ttOpenDelay.SetToolTip(pictureBox3, "Enable/Disable AUTO configuration of VSPE");
            // 
            // chkAutoConfigSSPort
            // 
            chkAutoConfigSSPort.AutoSize = true;
            chkAutoConfigSSPort.Location = new Point(7, 37);
            chkAutoConfigSSPort.Name = "chkAutoConfigSSPort";
            chkAutoConfigSSPort.Size = new Size(100, 19);
            chkAutoConfigSSPort.TabIndex = 33;
            chkAutoConfigSSPort.Text = "Simply Switch";
            chkAutoConfigSSPort.UseVisualStyleBackColor = true;
            // 
            // btnAutoSetupSS
            // 
            btnAutoSetupSS.Location = new Point(122, 35);
            btnAutoSetupSS.Name = "btnAutoSetupSS";
            btnAutoSetupSS.Size = new Size(75, 23);
            btnAutoSetupSS.TabIndex = 33;
            btnAutoSetupSS.Text = "Detect";
            btnAutoSetupSS.UseVisualStyleBackColor = true;
            btnAutoSetupSS.Click += btnAutoSetupSS_Click;
            // 
            // btnAutoSetupVSPE
            // 
            btnAutoSetupVSPE.Location = new Point(122, 64);
            btnAutoSetupVSPE.Name = "btnAutoSetupVSPE";
            btnAutoSetupVSPE.Size = new Size(75, 23);
            btnAutoSetupVSPE.TabIndex = 38;
            btnAutoSetupVSPE.Text = "Create";
            btnAutoSetupVSPE.UseVisualStyleBackColor = true;
            btnAutoSetupVSPE.Click += btnAutoSetupVSPE_Click;
            // 
            // panel6
            // 
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(label7);
            panel6.Controls.Add(txtVSPEConfigPath);
            panel6.Controls.Add(btnBrowsePathVSPE);
            panel6.Controls.Add(label6);
            panel6.Controls.Add(pictureBox3);
            panel6.Controls.Add(pictureBox1);
            panel6.Controls.Add(btnAutoSetupVSPE);
            panel6.Controls.Add(textBoxExt5);
            panel6.Controls.Add(btnAutoSetupSS);
            panel6.Controls.Add(chkAutoConfigSSPort);
            panel6.Location = new Point(251, 251);
            panel6.Margin = new Padding(4, 3, 4, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(233, 151);
            panel6.TabIndex = 33;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(7, 68);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(72, 15);
            label7.TabIndex = 41;
            label7.Text = "VSPE Config";
            // 
            // txtVSPEConfigPath
            // 
            txtVSPEConfigPath.Location = new Point(7, 122);
            txtVSPEConfigPath.Margin = new Padding(4, 3, 4, 3);
            txtVSPEConfigPath.Name = "txtVSPEConfigPath";
            txtVSPEConfigPath.ReadOnly = true;
            txtVSPEConfigPath.Size = new Size(220, 23);
            txtVSPEConfigPath.TabIndex = 35;
            txtVSPEConfigPath.Text = "%AppData%\\";
            // 
            // btnBrowsePathVSPE
            // 
            btnBrowsePathVSPE.Location = new Point(122, 93);
            btnBrowsePathVSPE.Name = "btnBrowsePathVSPE";
            btnBrowsePathVSPE.Size = new Size(75, 23);
            btnBrowsePathVSPE.TabIndex = 40;
            btnBrowsePathVSPE.Text = "Browse";
            btnBrowsePathVSPE.UseVisualStyleBackColor = true;
            btnBrowsePathVSPE.Click += btnBrowsePathVSPE_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(7, 97);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(60, 15);
            label6.TabIndex = 36;
            label6.Text = "VSPE Path";
            // 
            // textBoxExt5
            // 
            textBoxExt5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxExt5.BackColor = Color.FromArgb(93, 92, 97);
            textBoxExt5.BeforeTouchSize = new Size(227, 17);
            textBoxExt5.BorderStyle = BorderStyle.None;
            textBoxExt5.Font = new Font("Calibri Light", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxExt5.ForeColor = Color.White;
            textBoxExt5.Location = new Point(2, 3);
            textBoxExt5.Margin = new Padding(4, 3, 4, 3);
            textBoxExt5.Name = "textBoxExt5";
            textBoxExt5.Size = new Size(227, 17);
            textBoxExt5.TabIndex = 26;
            textBoxExt5.Text = "Enable/Disable Automated Port Setup";
            textBoxExt5.TextAlign = HorizontalAlignment.Center;
            // 
            // btnGenerateVSPEConfig
            // 
            btnGenerateVSPEConfig.Location = new Point(578, 321);
            btnGenerateVSPEConfig.Name = "btnGenerateVSPEConfig";
            btnGenerateVSPEConfig.Size = new Size(75, 23);
            btnGenerateVSPEConfig.TabIndex = 41;
            btnGenerateVSPEConfig.Text = "Create";
            btnGenerateVSPEConfig.UseVisualStyleBackColor = true;
            btnGenerateVSPEConfig.Click += btnGenerateVSPEConfig_Click;
            // 
            // button1
            // 
            button1.Location = new Point(497, 321);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 42;
            button1.Text = "Get Ports";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.AddToRecent = false;
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.ApplicationData;
            // 
            // frmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(737, 461);
            Controls.Add(button1);
            Controls.Add(btnGenerateVSPEConfig);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new Size(753, 600);
            MinimizeBox = false;
            Name = "frmSettings";
            ShowInTaskbar = false;
            TopMost = true;
            FormClosing += frmSettings_Closing;
            Shown += frmSettings_Shown;
            ((System.ComponentModel.ISupportInitialize)toggleisWirelessConnection).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxExt1).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt2).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picToolTip1).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxExt6).EndInit();
            ((System.ComponentModel.ISupportInitialize)picToolTip3).EndInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt3).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxExt7).EndInit();
            ((System.ComponentModel.ISupportInitialize)textBoxExt4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)textBoxExt5).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TextBox txtPort2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSerialPortPairBaudrate;
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
        private System.Windows.Forms.TextBox txtSerialPortSimplySwitchName;
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
        private Button btnAutoSetupVSPE;
        private Button btnAutoSetupSS;
        private CheckBox chkAutoConfigSSPort;
        private Panel panel6;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt5;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private TextBox txtVSPEConfigPath;
        private Button btnBrowsePathVSPE;
        private Label label6;
        private Button btnGenerateVSPEConfig;
        private Button button1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label7;
        private Label label12;
        private TextBox txtPort1;
        private Label lblSSPortDetectStatus;
    }
}

