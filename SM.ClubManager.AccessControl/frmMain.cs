using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SIS.Library.Base;
using SIS.Library.Base.Communication;
using SIS.Library.Base.Infrastructure.Extensions;
using System.Threading;
using SIS.Library.Base.Infrastructure.WindowsFormsExtensions;

using SerialPortLib;
using SM.ClubManager.AccessControl.Database;
using SM.ClubManager.AccessControl.Config;

namespace SM.ClubManager.AccessControl
{
   
    public partial class frmMain : Form
    {
        //
        string commandWifiOpenStringFormat = "";
        string commandWifiCloseStringFormat = "";
        string commandSerialOpenStringFormat = "";
        string commandSerialCloseStringFormat = "";

        //SimpleSerialClient serialInClient = null;
        SerialPortInput serialInClient = new SerialPortInput();
        //SerialPortStream serialInClient = new SerialPortStream();
        frmSettings settingsForm = new frmSettings();
        List<byte> messageBuffer = null;
        System.Threading.Thread comThread;
        string eofString = "\n\r\n";        
        public frmMain()
        {
            InitializeComponent();              

            Initialize();
        }

        private void Initialize()
        {
            Log("Initializing user interface");
            this.ShowInTaskbar = ConfigurationManager.AppSettings["IsDisplayInTaskBar"].ToBool();         

            Log("Intializing database");
            System.Data.Entity.Database.SetInitializer(new DatabaseInitializer());
                        
            Log("Initializing configuration...");
            SetDefaults();
          
            commandWifiOpenStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Wifi.Open"];
            commandWifiCloseStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Wifi.Close"];
            commandSerialOpenStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Serial.Close"];
            commandSerialCloseStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Serial.Close"];

            Log("Startup completed");
        }

        private void InitSerialComms()
        {            
            
            InitializeSerialInConnection();

            string portName = ApplicationSettings.Instance.SerialInPort;
            int portBaudRate = ApplicationSettings.Instance.SerialInBaudRate;

            OpenComPort(portName, portBaudRate);
        }

        #region Private methods

        private void InitializeSerialInConnection()
        {
            try
            {
                Log("Initializing serial connection...");
               
                if (serialInClient == null)
                {
                    serialInClient = new SerialPortInput();                    
                    serialInClient.ConnectionStatusChanged += SerialInClient_ConnectionStatusChanged;
                    serialInClient.MessageReceived += SerialInClient_MessageReceived;
                    
                    messageBuffer = new List<byte>();
                }              
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
                Log("Restart computer or enter another COM port for the serial IN connection", true);
            }            
        }

        private void SetDefaults()
        {
            try
            {
                if (string.IsNullOrEmpty(ApplicationSettings.Instance.SerialInPort))
                {
                    ApplicationSettings.Instance.SerialInPort = "COM1";
                }

                if (ApplicationSettings.Instance.SerialInBaudRate == default(int) || ApplicationSettings.Instance.SerialInBaudRate <= 0)
                {
                    ApplicationSettings.Instance.SerialInBaudRate = 9600;
                }

                if (string.IsNullOrEmpty(ApplicationSettings.Instance.WirelessDeviceIPAddress))
                {
                    ApplicationSettings.Instance.WirelessDeviceIPAddress = "192.168.0.65";
                }

                if (string.IsNullOrEmpty(ApplicationSettings.Instance.WirelessDevicePort))
                {
                    ApplicationSettings.Instance.WirelessDevicePort = "80";
                }

                if (ApplicationSettings.Instance.InchingDelay <= 0)
                {
                    ApplicationSettings.Instance.InchingDelay = 2;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
                 
        }

        private void Log(string msg, bool isError = false)
        {
            lstLog.InvokeIfRequired(t => t.AddEntry(msg, isError));
        }
        #endregion

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    //SaveConfigurationValues();
            //}
            //catch (Exception ex)
            //{
            //    Log(ex.Message, true);
            //}

            try
            {
                if (comThread != null)
                {
                    frmLoading.ShowLoadingForm();
                    comThread.Abort();
                    comThread.Join();
                    comThread = null;
                    frmLoading.CloseForm();
                    System.Threading.Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                Log("Error closing form: " + ex.Message, true);
                e.Cancel = true;
            }
        }

        //private void SaveConfigurationValues()
        //{
        //    try
        //    {
        //        Log("Saving configuration");
        //        //ApplicationSettings.Instance.SerialInPort = txtSwOutPort.Text;
        //        //ApplicationSettings.Instance.SerialInBaudRate = Convert.ToInt32(txtSerialInBaudrate.Text);
        //        //ApplicationSettings.Instance.WirelessDeviceIPAddress = txtIPAddress.Text;
        //        //ApplicationSettings.Instance.WirelessDevicePort = txtPort.Text;
        //        //ApplicationSettings.Instance.IsTargetWireless = rdoWirelessComms.Checked;
        //        //ApplicationSettings.Instance.InchingDelay = txtInchingDelay.Text.ToInt();

              
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex.Message, true);                
        //    }           
        //}

        private void frmMain_Shown(object sender, EventArgs e)
        {
            frmSplash.CloseForm();
            if(ConfigurationManager.AppSettings["IsDisplayFormOnStartup"].ToBool())
            {                
                this.Activate();
                this.Focus();
            }

            Log("Initialzing communication layer");
            InitSerialComms();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //SaveConfigurationValues();

                //depending on the wireless / wired method selected we choose the logic to execute
                //first we get the type of command that was sent i.e. ON / OFF  
                //if (rdoWirelessComms.Checked)
                //{
                ExecuteWirelessCommand();
                //}
                //else
                //{
                //    //First we validate input
                //    string cmdString = txtTx.Text.Trim();
                //    if (string.IsNullOrEmpty(cmdString))
                //    {
                //        Log("Tx value cannot be empty. Check the command format", true);
                //        return;
                //    }

                //    if ((!cmdString.ToUpper().StartsWith("N") && !cmdString.ToUpper().StartsWith("F")) || cmdString.Length < 2)
                //    {
                //        Log("Invalid command format", true);
                //        return;
                //    }
                //    int portNumber = -1;
                //    bool isNumber = int.TryParse(cmdString.Substring(1), out portNumber);
                //    if (!isNumber)
                //    {
                //        Log("Invalid command format", true);
                //        return;
                //    }

                //    ExecuteWiredCommand(cmdString.Substring(0, 1).ToUpper(), portNumber);
                //}
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }                                               
        }

        //private void ExecuteWiredCommand(string commandType, int portNumber)
        //{
        //    try
        //    {
        //        string port = ApplicationSettings.Instance.SerialInBaudRate;

        //        Log(string.Format("Executing wired command {0} on relay {1} on serial port {2}", commandType, portNumber.ToString(), port));
        //        using (SimpleSerialClient serial = new SimpleSerialClient(port))
        //        {
        //            serial.OpenConn();
        //            serial.Write(commandType + portNumber.ToString());
        //            serial.CloseConn();
        //        }              
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex.Message, true);
        //    }
        //}

        private void ExecuteWirelessCommand()
        {
            try
            {
                Log("Executing wireless command");                
                frmLoading.ShowLoadingForm("Executing wireless command...");
                //buid url
                string url = commandWifiCloseStringFormat;                    
                url = url.ToLower()
                            .Replace("{ip}", ApplicationSettings.Instance.WirelessDeviceIPAddress)
                            .Replace("{port}", ApplicationSettings.Instance.WirelessDevicePort)
                            .Replace("{delay}", ApplicationSettings.Instance.InchingDelay.ToString());
                HttpPost(url);
            }
            catch (Exception ex)
            {

                Log(ex.Message, true);
            }
            finally
            {
                frmLoading.CloseForm();
            }
        }

        private void HttpPost(string url)
        {
            try
            {                
                var request = (HttpWebRequest)WebRequest.Create(url);

                var postData = "username=" + Uri.EscapeDataString("myUser");
                postData += "&password=" + Uri.EscapeDataString("myPassword");
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }           
        }       

        #region ThreadMethods
        private void OpenComPort(string portName, int baudRate)
        {
            //string portName = ApplicationSettings.Instance.SerialInPort;
            //int portBaudRate = ApplicationSettings.Instance.SerialInBaudRate;
            try
            {
                Log(string.Format("Opening port {0}...", portName));
                
                //int count = 0;
                //bool isConnectionOk = false;
                //while (!isConnectionOk && count < 5)
                //{
                try
                {                        
                    if(serialInClient.IsConnected)
                    {
                        serialInClient.Disconnect();
                       
                        serialInClient.SetPort(portName: portName, baudRate: baudRate);

                        if (messageBuffer != null)
                        {
                            messageBuffer.Clear();
                        }

                        messageBuffer = null;
                        messageBuffer = new List<byte>();

                        serialInClient.Connect();
                    }                   
              
                if (serialInClient != null && serialInClient.IsConnected)
                {
                    Log(string.Format("Port {0} opened OK", portName));
                    rdoSerialInOK.Checked = true;
                }
                else
                {
                    rdoSerialInOK.Checked = false;
                    Log(string.Format("ERROR opening port {0}", portName), true);
                }
            }
            catch (Exception serialEx)
                {
                    Log("Error opening COM port: " + serialEx.Message, true);                        
                }                   
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }

            return;
        }

        private void SerialInClient_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs args)
        {
            Log("COM port connection status changed to '" + args.Connected.ToString() + "'");
        }

        private void SerialInClient_MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            try
            {
                //add data to the messagebuffer.            
                if (args.Data != null && args.Data.Count() > 0)
                {
                    foreach (var item in args.Data)
                    {
                        messageBuffer.Add(item);
                    }

                    //put entire buffer into string variable, then we look for the EOF byte sequence i.e. char(10)char(13)char(10) i.e. "\n\r\n"
                    List<RelayCommand> relayCommands = GetCommandsFromBuffer(ref messageBuffer, eofString);

                    if (relayCommands != null && relayCommands.Count() > 0)
                    {
                        Log(String.Format("Processing {0} commands", relayCommands.Count));
                        foreach (var item in relayCommands)
                        {
                            OnRelayCommandReceived(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
                 
        }

        private void OnRelayCommandReceived(RelayCommand item)
        {
            Log(string.Format("Processing relay command '{0}'", item.Command.ToString()));
            //check what type of command is required i.e. serial / wifi
            bool isWireless = ApplicationSettings.Instance.IsTargetWireless;


            switch (item.Command)
            {
                case RelayCommand.CommandType.Close:                   
                    break;
                case RelayCommand.CommandType.Open:
                    break;
                default:
                    break;
            }

            Log("TO BE COMPLETED");
            Log("Relay command processing completed");
        }
        
        private List<RelayCommand> GetCommandsFromBuffer(ref List<byte> byteList, string eofString)
        {
            try
            {
                Log("Looking for commands to process");
                // string str = Encoding.Default.GetString(bytes);
                string strBuffer = Encoding.UTF8.GetString(byteList.ToArray());

                if (strBuffer.Contains(eofString))
                {
                    //find the location of the first eofString so that we can cut the items before that out and then return the remaining bytes
                    //N1...F1...N1...F
                    var listOfCommands = strBuffer.Split(new string[] { eofString }, StringSplitOptions.RemoveEmptyEntries);             
                 
                    if (listOfCommands != null && listOfCommands.Count() > 0)
                    {                       
                        List<RelayCommand> relayCommands = new List<RelayCommand>();
                        //split the buffer into the various commands
                        foreach (var item in listOfCommands)
                        {
                            var relayCommand = RelayCommand.Create(item);
                            relayCommands.Add(relayCommand);
                        }
                       
                        //we find the location of the last occurence of the eofString
                        //deduct the index + eofString.Length from the total byteList
                        int lastEofStringIndex = strBuffer.LastIndexOf(eofString);
                        int eofStringCount = eofString.Count();
                        int removeIndexPosition = lastEofStringIndex + eofStringCount;

                        byteList.RemoveRange(0, removeIndexPosition);

                        return relayCommands;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        private void ProcessRelayCommand(RelayCommand item)
        {
            //show loading form

            string message = "Sending message to turnstile controller";
            frmLoading.ShowLoadingForm(message);
            Log(message);
        }

        //private void SerialInClient_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    Log(sender.ToString() + ": " + e.ToString());
        //    //throw new NotImplementedException();
        //}

        private void SerialInClient_OnSerialReceiving(object sender, SerialMessageEventArgs e)
        {            
            Log("Serial data received", false);
            if (e.SerialMessage.StartsWith("N"))
            {
                btnSend_Click(null,null);
            }
        }
        #endregion

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Log("Opening system configuration");
            DialogResult result = settingsForm.ShowDialog();    
            
            if(result == DialogResult.OK)
            {
                Log("New configuration values loaded");

                string portName = ApplicationSettings.Instance.SerialInPort;
                int portBaudRate = ApplicationSettings.Instance.SerialInBaudRate;
                OpenComPort(portName, portBaudRate);
            }
            else
            {
                Log("Configuration values NOT changed");
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cleanup();           
        }

        private void Cleanup()
        {
            if(settingsForm != null)
            {
                settingsForm.Close();
                settingsForm.Dispose();
                settingsForm = null;
            }

            if(serialInClient != null)
            {                
                if (serialInClient.IsConnected)
                {                    
                    serialInClient.Disconnect();
                }
                
                serialInClient.ConnectionStatusChanged -= SerialInClient_ConnectionStatusChanged;
                serialInClient.MessageReceived -= SerialInClient_MessageReceived;
                serialInClient = null;
            }

            lstLog.Dispose();
            lstLog = null;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnResetCOM_Click(object sender, EventArgs e)
        {
            string portName = ApplicationSettings.Instance.SerialInPort;
            int portBaudRate = ApplicationSettings.Instance.SerialInBaudRate;
            OpenComPort(portName, portBaudRate);
        }
    }
}
