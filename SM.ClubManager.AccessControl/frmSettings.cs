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
                SaveConfigurationValues();

                //e.Cancel = true;  
                
                this.Hide();               
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }          
        }

        private void SaveConfigurationValues()
        {
            try
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

            }
            catch (Exception ex)
            {
                Log(ex.Message, true);                
            }           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfigurationValues();
            this.DialogResult = DialogResult.OK;
            this.Hide();
                    
        }

       
       
    }
}
