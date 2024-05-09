using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Gibraltar;
using Newtonsoft.Json.Linq;
using NLog.Common;
using SIS.Library.Base.Infrastructure.Extensions;
using SM.ClubManager.AccessControl.Config;
using SM.ClubManager.AccessControl.UI.Infrastructure;
using Syncfusion.Windows.Forms.Tools;
using SM.ClubManager.AccessControl.PortScanner;

namespace SM.ClubManager.AccessControl
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            lblSSPortDetectStatus.Text = "";

            Log("Loading configuration...");
            try
            {
                txtPort1.Text = ApplicationSettings.Instance.SerialPort1Name;
                txtPort2.Text = ApplicationSettings.Instance.SerialPort2Name;
                txtSerialPortSimplySwitchName.Text = ApplicationSettings.Instance.SerialPortSimplySwitchName;
                txtSerialPortPairBaudrate.Text = ApplicationSettings.Instance.SerialPortPairBaudRate.ToString();
                txtIPAddress.Text = ApplicationSettings.Instance.WirelessDeviceIPAddress;
                txtWifiPort.Text = ApplicationSettings.Instance.WirelessDevicePort;
                txtInchingDelay.Text = ApplicationSettings.Instance.InchingDelay.ToString();
                txtSerialOutBaudRate.Text = ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate.ToString();
                toggleisWirelessConnection.ToggleState = ApplicationSettings.Instance.IsTargetWireless ? ToggleButtonState.Active : ToggleButtonState.Inactive;
                try
                {
                    chkAutoConfigSSPort.Checked = ApplicationSettings.Instance.isAutoConfigSimplySwitchPort;
                }
                catch (Exception ex1)
                {
                    ApplicationSettings.Instance.isAutoConfigSimplySwitchPort = false;
                    chkAutoConfigSSPort.Checked = ApplicationSettings.Instance.isAutoConfigSimplySwitchPort;
                }

                try
                {
                    if (ApplicationSettings.Instance.VSPEConfigPath == null || ApplicationSettings.Instance.VSPEConfigPath == "")
                    {
                        ApplicationSettings.Instance.VSPEConfigPath = Directory.GetCurrentDirectory(); ////Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SimplySwitch");
                    }
                    txtVSPEConfigPath.Text = ApplicationSettings.Instance.VSPEConfigPath;
                }
                catch (Exception)
                {
                    ApplicationSettings.Instance.VSPEConfigPath = Directory.GetCurrentDirectory();//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SimplySwitch");
                    txtVSPEConfigPath.Text = ApplicationSettings.Instance.VSPEConfigPath;
                }

            }
            catch (Exception)
            {

            }


            EnableDisableWifiAndUsbSettingControls(toggleisWirelessConnection.ToggleState == ToggleButtonState.Active);
        }

        #region Private methods


        private void Log(string msg, bool isError = false)
        {
            //lstLog.AddEntry(msg, isError);
        }
        #endregion

        private void frmSettings_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bool isSaveOK = SaveConfigurationValues();

                if (isSaveOK)
                {
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
        }

        private bool SaveConfigurationValues()
        {
            bool isSaveOK = false;
            try
            {

                Log("Validing settings before saving");
                if (!IsValidationError())
                {
                    Log("Saving configuration");
                    ApplicationSettings.Instance.SerialPort1Name = txtPort1.Text.Trim();
                    ApplicationSettings.Instance.SerialPort2Name = txtPort2.Text.Trim();
                    ApplicationSettings.Instance.SerialPortPairBaudRate = Convert.ToInt32(txtSerialPortPairBaudrate.Text);
                    ApplicationSettings.Instance.WirelessDeviceIPAddress = txtIPAddress.Text;
                    ApplicationSettings.Instance.WirelessDevicePort = txtWifiPort.Text;
                    ApplicationSettings.Instance.IsTargetWireless = toggleisWirelessConnection.ToggleState == ToggleButtonState.Active ? true : false;
                    bool isWireless = ApplicationSettings.Instance.IsTargetWireless;
                    ApplicationSettings.Instance.InchingDelay = txtInchingDelay.Text.ToInt();
                    ApplicationSettings.Instance.SerialPortSimplySwitchName = txtSerialPortSimplySwitchName.Text.Trim();
                    ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate = Convert.ToInt32(txtSerialOutBaudRate.Text.Trim());
                    ApplicationSettings.Instance.isAutoConfigSimplySwitchPort = chkAutoConfigSSPort.Checked;
                    ApplicationSettings.Instance.VSPEConfigPath = txtVSPEConfigPath.Text;

                    isSaveOK = true;
                }
                else
                {
                    Log("Validation failed");
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
                isSaveOK = false;
            }

            return isSaveOK;
        }

        private bool IsValidationError()
        {
            bool isError = false;
            if (string.IsNullOrEmpty(txtPort1.Text.Trim()))
            {
                txtPort1.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }

            if (string.IsNullOrEmpty(txtPort2.Text.Trim()))
            {
                txtPort2.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }

            if (string.IsNullOrEmpty(txtSerialPortPairBaudrate.Text))
            {
                txtSerialPortPairBaudrate.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }

            if (string.IsNullOrEmpty(txtIPAddress.Text.Trim()))
            {
                txtIPAddress.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }

            if (string.IsNullOrEmpty(txtWifiPort.Text.Trim()))
            {
                txtWifiPort.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }
            if (string.IsNullOrEmpty(txtInchingDelay.Text.Trim()))
            {
                txtInchingDelay.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }
            else
            {
                int delay = 0;
                try
                {
                    delay = txtInchingDelay.Text.ToDecimal().ToInt();
                    txtInchingDelay.Text = delay.ToString();
                }
                catch (Exception ex)
                {
                    txtInchingDelay.BackColor = System.Drawing.Color.Salmon;
                    isError = true;
                }

                if (delay < 0 || delay > 10)
                {
                    txtInchingDelay.BackColor = System.Drawing.Color.Salmon;
                    isError = true;
                }
            }

            if (string.IsNullOrEmpty(txtSerialPortSimplySwitchName.Text.Trim()))
            {
                txtSerialPortSimplySwitchName.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }
            if (string.IsNullOrEmpty(txtSerialOutBaudRate.Text.Trim()))
            {
                txtSerialOutBaudRate.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }

            if (string.IsNullOrEmpty(txtVSPEConfigPath.Text.Trim()))
            {
                txtVSPEConfigPath.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }

            return isError;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //VSPEManager.KillProcess();
            if (!IsComPortsValid())
            {
                return;
            }

            //if(!CreateVSPEConfig())
            //{
            //    return;
            //}

            bool isSaveOK = SaveConfigurationValues();
            if (isSaveOK)
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
            lblSSPortDetectStatus.Text = "";
        }

        private void EnableDisableWifiAndUsbSettingControls(bool isWifiEnabled)
        {

            txtIPAddress.Enabled = isWifiEnabled;
            txtWifiPort.Enabled = isWifiEnabled;

            txtSerialOutBaudRate.Enabled = !isWifiEnabled;
            txtSerialPortSimplySwitchName.Enabled = !isWifiEnabled;
        }

        private void toggleisWirelessConnection_ToggleStateChanged(object sender, ToggleStateChangedEventArgs e)
        {
            EnableDisableWifiAndUsbSettingControls(toggleisWirelessConnection.ToggleState == ToggleButtonState.Active);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.BackColor = System.Drawing.Color.White;
            }

        }

        private void picToolTip3_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowsePathVSPE_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog(this))
            {
                string path = folderBrowserDialog1.SelectedPath;
                txtVSPEConfigPath.Text = path;
            }
        }

        private void btnGenerateVSPEConfig_Click(object sender, EventArgs e)
        {
            try
            {
                //kill VSPE
                // If it is running, kill it                
                VSPEManager.KillProcess();
                VSPEManager v = new VSPEManager();
                var path = @txtVSPEConfigPath.Text;
                v.CreateVSPEConfig(8, 7, path);

                MessageBox.Show("VSPE config generated successfully");
            }
            catch (Exception ex)
            {
                Log("Error generating VSPE config file. " + ex.Message, true);
            }
        }

        internal uint GetPortNumberFromString(string portName)
        {
            try
            {
                string p1 = portName.ToUpper().Replace("COM", "");

                //check that
                uint portNumber = Convert.ToUInt16(p1);
                return portNumber;
            }
            catch
            {
                throw;
            }
        }

        private bool IsComPortsValid()
        {
            bool result = false;
            var p1 = txtPort1.Text.Trim().ToUpper();
            var p2 = txtPort2.Text.Trim().ToUpper();

            if (p1 == p2)
            {
                MessageBox.Show("Port 1 and Port 2 values cannot be the same");
                return result;
            }

            //validate com port values
            if (!IsValidPortFormat(p1))
            {
                MessageBox.Show("Invalid value for port 1.\r\nValue should be COMx, where x is a number from 1-9.");
                return result;
            }

            //validate com port values
            if (!IsValidPortFormat(p2))
            {
                MessageBox.Show("Invalid value port 2.\r\nValue should be COMx, where x is a number from 1-9.");
                return result;
            }
            result = true;
            return result;
        }

        private bool IsValidPortFormat(string value)
        {
            // Define the pattern using a regular expression
            string pattern = @"^COM[1-9]$";

            // Check if the input value matches the pattern
            return Regex.IsMatch(value, pattern);
        }

        private void btnAutoSetupVSPE_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("The software will need to close to complete the operation", "Are you sure", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            ApplicationSettings.Instance.VSPEConfigPath = txtVSPEConfigPath.Text;
            if (result == DialogResult.Yes)
            {
                if (CreateVSPEConfig())
                {
                    Application.Exit();
                }
            }
        }

        private bool CreateVSPEConfig()
        {
            //kill vspe
            //check that config com ports are not being used by another device in system
            //if used by another device WARN 
            //
            bool result = false;
            try
            {
                if (!IsComPortsValid())
                {
                    return result;
                }

                List<uint> portsInUse = PortUtilities.GetComPortsInUseAsNumbers();
                uint p1 = GetPortNumberFromString(txtPort1.Text);
                uint p2 = GetPortNumberFromString(txtPort2.Text);
                string portsAvail = "";
                GetAllAvailableComPorts().ForEach(port => { portsAvail = portsAvail.TrimStart(',', ' ') + ", " + port; });
                if (portsInUse.Contains(p1))
                {
                    MessageBox.Show("Invalid value for port 1. Port in use. Choose another port.\r\nAvailable ports are: " + portsAvail + ". \r\n\r\nDo you want to continue anyway?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    return result;
                }

                if (portsInUse.Contains(p2))
                {
                    MessageBox.Show("Invalid value for port 2. Port in use. Choose another port.\r\nAvailable ports are:  " + portsAvail + ". \r\n\r\nDo you want to continue anyway?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    return result;
                }

                if (p1 == p2)
                {
                    MessageBox.Show("Port 1 and Port 2 values cannot be the same");
                    return result;
                }

                VSPEManager v = new VSPEManager();
                //v.KillProcess();
                /*
                 * this automatically creates a vspe pair based on what's avail on the system
                 */
                //List<uint> availablePorts = GetAllAvailableComPorts();
                //if(availablePorts.Count == 0) 
                //{
                //    MessageBox.Show("Not enough ports available to generate VSPE configuration. Call support.","Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                //    return;
                //}
                //uint port1 = availablePorts[0];
                //uint port2 = availablePorts[1];

                //if ( string.IsNullOrEmpty(ApplicationSettings.Instance.SerialPort2Name) || string.IsNullOrEmpty(ApplicationSettings.Instance.SerialPortSimplySwitchName))
                //{
                //    string msg = "Club Manager and USB port values cannot be empty";
                //    MessageBox.Show(msg);
                //    StartVSPE();
                //    return;
                //}


                if (string.IsNullOrEmpty(txtPort1.Text) || string.IsNullOrEmpty(txtPort2.Text))
                {
                    string msg = "Club Manager and USB port values cannot be empty";
                    MessageBox.Show(msg);
                    v.StartVSPE();
                    return result;
                }

                try
                {
                    //string p1 = txtSwInPort.Text.ToUpper().Replace("COM", "");
                    //string p2 = txtSerialPortSimplySwitchName.Text.ToUpper().Replace("COM", "");
                    ////check that
                    //uint port1 = Convert.ToUInt16(p1);
                    //uint port2 = Convert.ToUInt16(p2);

                    var path = ApplicationSettings.Instance.VSPEConfigPath;// @txtVSPEConfigPath.Text;

                    v.CreateVSPEConfig(txtPort1.Text, txtPort2.Text, path);

                    //StartVSPE();
                }
                catch (Exception ex)
                {
                    Log(ex.Message, true);
                    string msg = "Make sure the formatting of the ports are correct. It need to be 'COMx', where x is a number from 1 - 9";
                    MessageBox.Show(msg);
                    v.StartVSPE();
                    return result;
                }

                ////Set the system config
                //txtSwInPort.Text = "COM" + port1.ToString();
                //txtSerialPortSimplySwitchName.Text = "COM" + port2.ToString();

                result = true;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<T> GetMissingItems<T>(List<T> knownPossibilities, List<T> items)
        {
            // Use LINQ to find items in knownPossibilities that are not in items
            List<T> missingItems = knownPossibilities.Except(items).ToList();
            return missingItems;
        }

        private List<uint> GetAllAvailableComPorts()
        {
            //Generate static list of possible ports
            List<uint> staticPorts = new List<uint>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var portsInUse = PortUtilities.GetComPortsInUseAsNumbers();
            //lstPortsList.Items.Clear();

            List<uint> availablePorts = GetMissingItems<uint>(staticPorts, portsInUse);

            return availablePorts;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAllAvailableComPorts();
        }

        private void btnAutoSetupSS_Click(object sender, EventArgs e)
        {
            var userMessage = MessageBox.Show("This will attempt to detect the Simply Switch controller. Make sure the controller is...\r\n\r\n- Plugged in\r\n- Switched on\r\n\r\nDo you want to continue?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (userMessage == DialogResult.Yes)
            {
                try
                {
                    txtSerialPortSimplySwitchName.Text = "";

                    frmSplash.ShowSplashScreen();

                    var mainForm = this.Owner as frmMain;
                    if (mainForm != null)
                    {
                        mainForm.simplySwitchClient.SDisconnect();
                        mainForm.simplySwitchClient.Dispose();
                    }
                    //get the list of ports in use by the system
                    var listPorts = PortUtilities.GetComPortsInUseAsStrings();

                    if (listPorts != null && listPorts.Count() > 0)
                    {
                        Scanner scanner = new Scanner();

                        string ssPort = scanner.FindDeviceByPort(listPorts);//, messageToSend, messageResponseExpected);

                        if (!string.IsNullOrEmpty(ssPort))
                        {
                            txtSerialPortSimplySwitchName.Text = ssPort;
                            lblSSPortDetectStatus.Text = "Found!";
                        }
                        else
                        {
                            lblSSPortDetectStatus.Text = "Not found!";
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                        lblSSPortDetectStatus.Text = "Error";
                        MessageBox.Show("No active ports found. Make sure the drivers are installed, the device is plugged in and switched on");
                    }
                }
                catch (Exception ex)
                {
                    Log(ex.Message, true);
                }

                frmSplash.CloseForm();
            }
        }

        private void TxtSerialPortSimplySwitchName_TextChanged(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void txtSerialPortSimplySwitchName_TextChanged(object sender, EventArgs e)
        {
            lblSSPortDetectStatus.Text = "";
        }

        private void frmSettings_Shown(object sender, EventArgs e)
        {
            lblSSPortDetectStatus.Text = "";
            txtSerialPortSimplySwitchName.Text = ApplicationSettings.Instance.SerialPortSimplySwitchName;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtVSPEConfigPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
