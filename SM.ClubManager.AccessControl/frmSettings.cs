using System;
using System.Windows.Forms;
using SIS.Library.Base.Infrastructure.Extensions;
using SM.ClubManager.AccessControl.Config;
using Syncfusion.Windows.Forms.Tools;

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
            Log("Loading configuration...");           
            txtSwInPort.Text = ApplicationSettings.Instance.SerialInPort;
            txtSerialOutPort.Text = ApplicationSettings.Instance.SerialOutPort;            
            txtSerialInBaudrate.Text = ApplicationSettings.Instance.SerialInBaudRate.ToString();
            txtIPAddress.Text = ApplicationSettings.Instance.WirelessDeviceIPAddress;
            txtWifiPort.Text = ApplicationSettings.Instance.WirelessDevicePort;            
            txtInchingDelay.Text = ApplicationSettings.Instance.InchingDelay.ToString();
            txtSerialOutBaudRate.Text = ApplicationSettings.Instance.SerialOutBaudRate.ToString();
            toggleisWirelessConnection.ToggleState = ApplicationSettings.Instance.IsTargetWireless ? ToggleButtonState.Active : ToggleButtonState.Inactive;

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
                
                if(isSaveOK)
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
                    ApplicationSettings.Instance.SerialInPort = txtSwInPort.Text.Trim();
                    ApplicationSettings.Instance.SerialInBaudRate = Convert.ToInt32(txtSerialInBaudrate.Text);
                    ApplicationSettings.Instance.WirelessDeviceIPAddress = txtIPAddress.Text;
                    ApplicationSettings.Instance.WirelessDevicePort = txtWifiPort.Text;
                    ApplicationSettings.Instance.IsTargetWireless = toggleisWirelessConnection.ToggleState == ToggleButtonState.Active ? true : false;
                    bool isWireless = ApplicationSettings.Instance.IsTargetWireless;
                    ApplicationSettings.Instance.InchingDelay = txtInchingDelay.Text.ToInt();
                    ApplicationSettings.Instance.SerialOutPort = txtSerialOutPort.Text.Trim();
                    ApplicationSettings.Instance.SerialOutBaudRate = Convert.ToInt32(txtSerialOutBaudRate.Text.Trim());
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
            if (string.IsNullOrEmpty(txtSwInPort.Text.Trim()))
            {
                txtSwInPort.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }
            
            if (string.IsNullOrEmpty(txtSerialInBaudrate.Text))
            {
                txtSerialInBaudrate.BackColor = System.Drawing.Color.Salmon;
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
              
                if(delay < 0 || delay > 10)
                {
                    txtInchingDelay.BackColor = System.Drawing.Color.Salmon;
                    isError = true;
                }                
            }

            if (string.IsNullOrEmpty(txtSerialOutPort.Text.Trim()))
            {
                txtSerialOutPort.BackColor = System.Drawing.Color.Salmon;
                isError = true;
            }
            if (string.IsNullOrEmpty(txtSerialOutBaudRate.Text.Trim()))
            {
                txtSerialOutBaudRate.BackColor = System.Drawing.Color.Salmon;
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
            bool isSaveOK = SaveConfigurationValues();
            if(isSaveOK)
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }                            
        }

        private void EnableDisableWifiAndUsbSettingControls(bool isWifiEnabled)
        {

            txtIPAddress.Enabled = isWifiEnabled;
            txtWifiPort.Enabled = isWifiEnabled;

            txtSerialOutBaudRate.Enabled = !isWifiEnabled;
            txtSerialOutPort.Enabled = !isWifiEnabled;
        }

        private void toggleisWirelessConnection_ToggleStateChanged(object sender, ToggleStateChangedEventArgs e)
        {
            EnableDisableWifiAndUsbSettingControls(toggleisWirelessConnection.ToggleState == ToggleButtonState.Active);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(tb != null)
            {
                tb.BackColor = System.Drawing.Color.White;
            }
           
        }
    }
}
