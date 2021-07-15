using System;
using System.Windows.Forms;
using SIS.Library.Base.Infrastructure.Extensions;
using SM.ClubManager.AccessControl.Config;

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
            txtSwOutPort.Text = ApplicationSettings.Instance.SerialInPort;
            txtSerialInBaudrate.Text = ApplicationSettings.Instance.SerialInBaudRate.ToString();
            txtIPAddress.Text = ApplicationSettings.Instance.WirelessDeviceIPAddress;
            txtWifiPort.Text = ApplicationSettings.Instance.WirelessDevicePort;            
            txtInchingDelay.Text = ApplicationSettings.Instance.InchingDelay.ToString();            
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
                ApplicationSettings.Instance.SerialInPort = txtSwOutPort.Text;
                ApplicationSettings.Instance.SerialInBaudRate = Convert.ToInt32(txtSerialInBaudrate.Text);
                ApplicationSettings.Instance.WirelessDeviceIPAddress = txtIPAddress.Text;
                ApplicationSettings.Instance.WirelessDevicePort = txtWifiPort.Text;
                ApplicationSettings.Instance.IsTargetWireless = true;
                ApplicationSettings.Instance.InchingDelay = txtInchingDelay.Text.ToInt();
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

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
