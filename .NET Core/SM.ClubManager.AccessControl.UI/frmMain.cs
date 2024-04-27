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
using System.Threading;
using SerialPortLib;
using SM.ClubManager.AccessControl.Database;
using SM.ClubManager.AccessControl.Config;
using System.Reflection;
using System.IO;
using SM.ClubManager.AccessControl.Infrastructure;
using System.Runtime.InteropServices;
using Gibraltar.Agent;
using System.Collections.Concurrent;
using SM.ClubManager.AccessControl.SDK;
using SIS.Library.Base.Infrastructure.Extensions;
using SM.ClubManager.AccessControl.Model;
using SM.ClubManager.AccessControl.UI.Infrastructure;
using SM.ClubManager.AccessControl.Repository;
using SM.ClubManager.AccessControl.PortScanner;
using Syncfusion.Windows.Forms.Tools;


namespace SM.ClubManager.AccessControl
{
#pragma warning disable IDE1006 // Naming Styles

    enum NotificationType
    {
        Error,
        Warning,
        Info,
        General
    }

    enum FormViewSize
    {
        Small = 0,
        ApplicationLogsOnly = 1,
        Full = 2
    }

    public partial class frmMain : Form
    {
        //set default view size
        FormViewSize formViewSize = FormViewSize.Small;

        Size sizeSmall = new Size(279, 325);
        Size sizeApplicationLogsOnly = new Size(716, 648);
        Size sizeFull = new Size(1190, 648);

        bool isClosing = false;

        //string commandWifiRelayOpenStringFormat = "";
        //string commandWifiRelayCloseStringFormat = "";
        //string commandSerialRelayOpenStringFormat = "";
        //string commandSerialRelayCloseStringFormat = "";


        SerialPortInput serialPort2 = null;//new SerialPortInput();
        internal SimplySwitch simplySwitchClient = new SimplySwitch();//serialOutClient = null;//new SerialPortInput();

        frmSettings settingsForm;
        frmNewLoading newLoadingForm = new frmNewLoading();
        List<byte> messageBuffer = null;
        //List<byte> usbMessageBuffer = null;
        System.Threading.Thread comThread;
        string eofString = "\n\r\n";
        Image imgUnchecked;
        Image imgChecked;
        Image imgUsbConnection;
        Image imgWifiConnection;
        Icon notificationWarningIcon;
        Icon notificationInformationIcon;
        Image imgInfo;

        public frmMain()
        {
            InitializeComponent();

            InitializeDefaultValues();

            Initialize();           
        }

        private void CheckSimplySwitchConnectionAndAutoDetect()
        {          
            if(!simplySwitchClient.IsConnected)            
            {
                if (ApplicationSettings.Instance.isAutoConfigSimplySwitchPort)
                {
                    string msg = "System could not connect to the Simply Switch controller. Do you want to try to detect the port automatically?";
                    string caption = "Do you want to continue?";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon icon = MessageBoxIcon.Question;

                    DialogResult result = MessageBox.Show(msg, caption, buttons, icon);

                    if (result == DialogResult.Yes)
                    {
                        
                        DialogResult scanConfirmation = DialogResult.Yes;
                        bool isDetected = false;
                        while (!isDetected && scanConfirmation == DialogResult.Yes)
                        {
                            frmSplash.ShowSplashScreen("Detecting controller..");

                            var ports = PortUtilities.GetComPortsInUseAsStrings();
                            Scanner scanner = new Scanner();                            
                            string port = scanner.FindDeviceByPort(ports);

                            frmSplash.CloseForm();

                            if (string.IsNullOrEmpty(port))
                            {
                                msg = "No controller detected. Make sure it's powered on and plugged in. Do you want to try again?";
                                caption = "Controller not found";
                                icon = MessageBoxIcon.Error;

                                MessageBox.Show(msg, caption,buttons, icon);                                
                            }
                            else
                            {
                                ApplicationSettings.Instance.SerialPortSimplySwitchName = port;
                                scanConfirmation = DialogResult.No;
                                isDetected = true;
                            }

                            if(isDetected)
                            {
                                InitializeSimplySwitchClient();
                                SimplySwitchConnect();
                            }
                        }                                               
                    }
                }                                               
            }           
        }

        private void InitializeDefaultValues()
        {
            if (string.IsNullOrEmpty(ApplicationSettings.Instance.SerialPort1Name))
            {
                ApplicationSettings.Instance.SerialPort1Name = "COM3";
            }

            if (string.IsNullOrEmpty(ApplicationSettings.Instance.SerialPort2Name))
            {
                ApplicationSettings.Instance.SerialPort2Name = "COM4";
            }

            try
            {
                int i = ApplicationSettings.Instance.SerialPortPairBaudRate;
            }
            catch (UnableToRetrieveConfigurationValueException ex)
            {
                ApplicationSettings.Instance.SerialPortPairBaudRate = 9600;
            }

            try
            {
                int i = ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate; 
            }
            catch (UnableToRetrieveConfigurationValueException ex)
            {
                ApplicationSettings.Instance.SerialPortPairBaudRate = 115200;                
            }
        }

        #region Init Methods

        private void Initialize()
        {
            formViewSize = FormViewSize.Small;
                        
            this.Size = sizeSmall;

            Log("Initializing user interface");
            this.ShowInTaskbar = ConfigurationManager.AppSettings["IsDisplayInTaskBar"].ToBool();
            
            //VSPEManager.KillProcess();

            VSPEManager vSPEManager = new VSPEManager();
            
            if(!vSPEManager.IsVSPEConfigExists(ApplicationSettings.Instance.VSPEConfigPath))
            {                
                MessageBox.Show("No valid VSPE config exists. Recreate VSPE configuration under 'Settings'.");                
            }
            //vSPEManager.CreateVSPEConfig(ApplicationSettings.Instance.SerialPort1Name, ApplicationSettings.Instance.SerialPort2Name, ApplicationSettings.Instance.VSPEConfigPath);
            vSPEManager.StartVSPE();
            settingsForm = new frmSettings();
            //Log("Intializing database");
            //System.Data.Entity.Database.SetInitializer(new DatabaseInitializer());

            Log("Preloading assets");
            PreloadAssets();

            Log("Assigning resources");
            AssignResources();

            Log("Initializing configuration...");
            SetDefaults();

            //commandWifiRelayOpenStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Wifi.RelayOpen"];
            //commandWifiRelayCloseStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Wifi.RelayClose"];
            //commandSerialRelayOpenStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Serial.RelayOpen"];
            //commandSerialRelayCloseStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Serial.RelayClose"];

            newLoadingForm.StartPosition = FormStartPosition.Manual;
            newLoadingForm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (newLoadingForm.Width / 2), Screen.PrimaryScreen.WorkingArea.Height - newLoadingForm.Height - 50);

            //StartCommandQueueConsumer();

            //if (simplySwitchClient == null)
            //{
            //    simplySwitchClient = new SimplySwitch();
            //}
            //simplySwitchClient.OnLogMessage += SimplySwitchClient_OnLogMessage;
            //simplySwitchClient.OnConnected += SimplySwitchClient_OnConnected;
            //simplySwitchClient.OnDisconnected += SimplySwitchClient_OnDisconnected;
            InitializeSimplySwitchClient();
            Log("Startup completed");
        }

        private void InitializeSimplySwitchClient()
        {
            if (simplySwitchClient != null)
            {
                simplySwitchClient.SDisconnect();
                simplySwitchClient.OnLogMessage -= SimplySwitchClient_OnLogMessage;
                simplySwitchClient.OnConnected -= SimplySwitchClient_OnConnected;
                simplySwitchClient.OnDisconnected -= SimplySwitchClient_OnDisconnected;
                simplySwitchClient.Dispose();
                simplySwitchClient = null;
            }
            simplySwitchClient = new SimplySwitch();
            simplySwitchClient.OnLogMessage += SimplySwitchClient_OnLogMessage;
            simplySwitchClient.OnConnected += SimplySwitchClient_OnConnected;
            simplySwitchClient.OnDisconnected += SimplySwitchClient_OnDisconnected;
        }

        private void AssignResources()
        {

        }

        private void ConfigureSystem()
        {
            //EnableDisableWirelessComms();
        }

        private void PreloadAssets()
        {
            try
            {
                imgInfo = LoadImageFromResource("SM.ClubManager.AccessControl.UI.Resources.info.png", Assembly.GetExecutingAssembly());
                imgUnchecked = LoadImageFromResource("SM.ClubManager.AccessControl.UI.Resources.unchecked.png", Assembly.GetExecutingAssembly());
                imgChecked = LoadImageFromResource("SM.ClubManager.AccessControl.UI.Resources.checked.png", Assembly.GetExecutingAssembly());
                imgUsbConnection = LoadImageFromResource("SM.ClubManager.AccessControl.UI.Resources.usb-connection.png", Assembly.GetExecutingAssembly());
                imgWifiConnection = LoadImageFromResource("SM.ClubManager.AccessControl.UI.Resources.wifi-connection.png", Assembly.GetExecutingAssembly());
                notificationInformationIcon = Icon;//LoadIconFromResource("SM.ClubManager.AccessControl.Resources.notification-warning-icon.ico", Assembly.GetExecutingAssembly());
                notificationWarningIcon = LoadIconFromResource("SM.ClubManager.AccessControl.UI.Resources.notification-warning-icon.ico", Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
        }
        
        private SSResponse SimplySwitchConnect()
        {
            SSResponse response = null;
            try
            {
                simplySwitchClient.SerialPort = ApplicationSettings.Instance.SerialPortSimplySwitchName;
                simplySwitchClient.SerialBaudRate = ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate;
                response = simplySwitchClient.SConnect();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        private void InitSerialComms()
        {
            //InitializeSerialOutConnection();

            InitializeSerialPort2Connection();

            string portName = ApplicationSettings.Instance.SerialPort2Name;
            int portBaudRate = ApplicationSettings.Instance.SerialPortPairBaudRate;

            OpenComPort(serialPort2, portName, portBaudRate, picInSerialConnection);

            InitializeSimplySwitchClient();

            if (!simplySwitchClient.IsConnected)
            {
                simplySwitchClient.SerialPort = ApplicationSettings.Instance.SerialPortSimplySwitchName;
                simplySwitchClient.SerialBaudRate = ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate;
                simplySwitchClient.SConnect();
            }
        }

        private void HandleSimplySwitchClientConnectionEvent(bool isConnectedEvent)
        {
            string statusString = isConnectedEvent ? "opened" : "closed"; //args.Connected.ToString();
            Log("USB port " + statusString);
            DisplayNotificationBalloon("Simply Switch", "USB connection " + statusString, isConnectedEvent ? NotificationType.Info : NotificationType.Warning);

            if (picConnectionType != null)
            {
                SetConnectionDisplayEnabledDisabled(picConnectionType, isConnectedEvent);
            }

            if (!isConnectedEvent)
            {
                rtbUsbOutput.InvokeIfRequired(t => t.BackColor = Color.White);
                rtbUsbOutput.InvokeIfRequired(t => t.ForeColor = Color.Black);
                txtUsbCommand.Select();
                txtUsbCommand.Focus();
                rtbUsbOutput.InvokeIfRequired(t => t.Clear());
            }
            else
            {
                grpUsbCommandPanel.InvokeIfRequired(t => t.Enabled = true);
                rtbUsbOutput.InvokeIfRequired(t => t.BackColor = Color.Black);
                rtbUsbOutput.InvokeIfRequired(t => t.ForeColor = Color.LimeGreen);
            }

            //usbMessageBuffer?.Clear();

            //usbMessageBuffer = null;
            //usbMessageBuffer = new List<byte>();

            picScanResult.InvokeIfRequired(t => t.Visible = false);
        }
        
        private void SimplySwitchClient_OnConnected(object? sender, EventArgs e)
        {
            HandleSimplySwitchClientConnectionEvent(true);
        }

        private void SimplySwitchClient_OnDisconnected(object? sender, EventArgs e)
        {
            HandleSimplySwitchClientConnectionEvent(false);
        }

        private void SimplySwitchClient_OnLogMessage(object? sender, SSLogMessage e)
        {

            if (e.IsDebug)
            {
                rtbUsbOutput.InvokeIfRequired(r =>
                {
                    r.AppendText(e.Message);
                    if (r.Lines.Length > 100)
                    {
                        r.Clear();
                    }
                });
            }
            else
            {
                Log(e.Message, e.IsError);
            }
        }

        public static Image LoadImageFromResource(string resourceName)
        {
            try
            {
                return LoadImageFromResource(resourceName, null);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Icon LoadIconFromResource(string resourceName, Assembly assembly = null)
        {
            try
            {
                if (assembly == null)
                {
                    assembly = Assembly.GetExecutingAssembly();
                }

                Stream _imageStream = assembly.GetManifestResourceStream(resourceName);
                return new Icon(_imageStream);
            }
            catch (Exception ex)
            {
                Log("Error loading assembly resource icon. " + ex.Message, true);
                return null;
            }
        }

        public static Image LoadImageFromResource(string resourceName, Assembly assembly)
        {
            try
            {
                if (assembly == null)
                {
                    assembly = Assembly.GetExecutingAssembly();
                }

                Stream _imageStream = assembly.GetManifestResourceStream(resourceName);
                return new Bitmap(_imageStream);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Private methods

        private void DisplayNotificationBalloon(string header, string message, NotificationType notificationType = NotificationType.Info)
        {
            Icon icon = this.Icon;
            ToolTipIcon ttIcon = ToolTipIcon.Info;

            //switch (notificationType)
            //{
            //    case NotificationType.Error:

            //        ttIcon = ToolTipIcon.Error;
            //        break;
            //    case NotificationType.Info:

            //        ttIcon = ToolTipIcon.Info;
            //        break;
            //    default:
            //        ttIcon = ToolTipIcon.None;
            //        break;
            //}
            
            toolStripStatusLabel1.Text = header;
            toolStripStatusLabel2.Text = " - ";
            toolStripStatusLabel3.Text = message;

            //NotifyIcon notifyIcon = new NotifyIcon
            //{
            //    Visible = true,
            //    Icon = icon,
            //    BalloonTipIcon = ttIcon,
            //    Text = ""
            //};
            //if (header != null)
            //{
            //    notifyIcon.BalloonTipTitle = header;
            //}
            //if (message != null)
            //{
            //    notifyIcon.BalloonTipText = message;
            //}

            //notifyIcon.BalloonTipClosed += (sender, args) => DisposeNotification(notifyIcon);
            //notifyIcon.BalloonTipClicked += (sender, args) => DisposeNotification(notifyIcon);
            //notifyIcon.ShowBalloonTip(0);
        }

        private void DisposeNotification(NotifyIcon notifyIcon)
        {
            notifyIcon.Dispose();
        }

        private void OpenComPort(SerialPortInput serialClient, string portName, int baudRate, PictureBox picBox = null)
        {
            try
            {
                Log(string.Format("Opening port {0}...", portName));

                try
                {
                    serialClient.Disconnect();

                    serialClient.SetPort(portName: portName, baudRate: baudRate);

                    serialClient.Connect();

                    if (serialClient != null && serialClient.IsConnected)
                    {
                        Log(string.Format("Port {0} opened OK", portName));
                        if (picBox != null)
                        {
                            SetConnectionDisplayEnabledDisabled(picBox, true);
                        }
                    }
                    else
                    {
                        if (picBox != null)
                        {
                            SetConnectionDisplayEnabledDisabled(picBox, false);
                        }

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

        private List<ISerialMessage> GetMessagesFromBuffer(ref List<byte> byteList, string eofString)
        {
            try
            {
                int preactivationdelay = ApplicationSettings.Instance.InchingDelay;
                // string str = Encoding.Default.GetString(bytes);
                string strBuffer = Encoding.UTF8.GetString(byteList.ToArray());

                if (strBuffer.Contains(eofString))
                {
                    //find the location of the first eofString so that we can cut the items before that out and then return the remaining bytes
                    //N1...F1...N1...F
                    var listOfMessages = strBuffer.Split(new string[] { eofString }, StringSplitOptions.RemoveEmptyEntries);

                    if (listOfMessages != null && listOfMessages.Count() > 0)
                    {
                        List<ISerialMessage> serialMessages = new List<ISerialMessage>();
                        //split the buffer into the various commands
                        foreach (var item in listOfMessages)
                        {
                            ISerialMessage serialMessage = new RelayCommand();
                            serialMessage = serialMessage.Create(item, preactivationdelay);
                            serialMessages.Add(serialMessage);
                        }

                        //we find the location of the last occurence of the eofString
                        //deduct the index + eofString.Length from the total byteList
                        int lastEofStringIndex = strBuffer.LastIndexOf(eofString);
                        int eofStringCount = eofString.Count();
                        int removeIndexPosition = lastEofStringIndex + eofStringCount;

                        byteList.RemoveRange(0, removeIndexPosition);

                        return serialMessages;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetConnectionDisplayEnabledDisabled(PictureBox picBox, bool isChecked = false)
        {
            if (isChecked)
            {
                picBox.Image = (Image)imgChecked.Clone();
            }
            else
            {
                picBox.Image = (Image)imgUnchecked.Clone();
            }
        }

        private void Cleanup()
        {
            isClosing = true;
            //commandQueue.CompleteAdding();
            //var items = commandQueue.Take(commandQueue.Count);
            //foreach (var item in items)
            //{
            //    item.Dispose();
            //}
            //items = null;
            
            VSPEManager.KillProcess();

            if (settingsForm != null)
            {
                settingsForm.Close();
                settingsForm.Dispose();
                settingsForm = null;
            }

            if (newLoadingForm != null)
            {
                newLoadingForm.Close();
                newLoadingForm.Dispose();
                newLoadingForm = null;
            }

            if (serialPort2 != null)
            {
                serialPort2.Disconnect();
                serialPort2.ConnectionStatusChanged -= SerialInClient_ConnectionStatusChanged;
                serialPort2.MessageReceived -= SerialInClient_MessageReceived;

                serialPort2 = null;
            }

            if(simplySwitchClient != null)
            {
                simplySwitchClient.OnLogMessage -= SimplySwitchClient_OnLogMessage;
                simplySwitchClient.OnConnected -= SimplySwitchClient_OnConnected;
                simplySwitchClient.OnDisconnected -= SimplySwitchClient_OnDisconnected;

                simplySwitchClient.Dispose();
                simplySwitchClient = null;
            }
            

            lstLog.Dispose();
            lstLog = null;
            //commandQueue.Dispose();
            //commandQueue = null;
        }

        private void ShowNewLoadingForm(bool displayLoader)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (newLoadingForm != null)
                    {
                        newLoadingForm.RunCounter(ApplicationSettings.Instance.InchingDelay);
                        newLoadingForm.Show();
                        newLoadingForm.TopMost = true;

                    }

                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (newLoadingForm != null)
                    {
                        newLoadingForm.Hide();
                    }

                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }

        private void ApplyPreActivationDelay(int delay)
        {
            try
            {
                int displayDelay = delay;
                delay *= 1000;

                if (delay > 0)
                {
                    // frmLoading.ShowLoadingForm(String.Format("Waiting for {0} seconds before activating the gate", displayDelay));
                    ShowNewLoadingForm(true);
                    //We have to add in additional 100ms in case th delay is too close to zero, because then the loading form display will cause issues
                    if (delay < 100)
                    {
                        delay += 100;
                    }

                    System.Threading.Thread.Sleep(delay);
                    ShowNewLoadingForm(false);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
        }


        private void OnRelayCommandReceived(ISerialMessage item)
        {
            if (!(item is RelayCommand relayCommand))
            {
                throw new Exception("Cannot process Club Manager command. Invalid command object received!");
            }

            Log(string.Format("Processing relay command '{0}'", relayCommand.Command.ToString()));
            //check what type of command is required i.e. serial / wifi
            //_ = ApplicationSettings.Instance.IsTargetWireless;

            //this method should not care about whether command is wireless / wired. it just wants to execute command
            try
            {
                var response = simplySwitchClient.SActivate();
                //commandQueue?.Add(relayCommand);
                //Adding delay to allow the 'loading' image to be displayed so user can see something happened. serves no other purpose
                Thread.Sleep(500);

            }
            catch (Exception)
            {
                throw;
            }

            Log("Relay command processing completed");
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

        private void InitializeSerialPort2Connection()
        {
            try
            {
                Log("Initializing serial IN connection object...");

                if (serialPort2 != null)
                {

                    serialPort2.Disconnect();
                    serialPort2.MessageReceived -= SerialInClient_MessageReceived;
                    serialPort2.ConnectionStatusChanged -= SerialInClient_ConnectionStatusChanged;
                    serialPort2 = null;
                }
                    serialPort2 = new SerialPortInput();
                    serialPort2.ConnectionStatusChanged += SerialInClient_ConnectionStatusChanged;
                    serialPort2.MessageReceived += SerialInClient_MessageReceived;                                  
                
                
                if (messageBuffer != null)
                {
                    messageBuffer.Clear();
                }
                else
                {
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
                if (string.IsNullOrEmpty(ApplicationSettings.Instance.SerialPort2Name))
                {
                    ApplicationSettings.Instance.SerialPort2Name = "COM1";
                }

                if (ApplicationSettings.Instance.SerialPortPairBaudRate == default || ApplicationSettings.Instance.SerialPortPairBaudRate <= 0)
                {
                    ApplicationSettings.Instance.SerialPortPairBaudRate = 9600;
                }

                if (string.IsNullOrEmpty(ApplicationSettings.Instance.WirelessDeviceIPAddress))
                {
                    ApplicationSettings.Instance.WirelessDeviceIPAddress = "192.168.0.65";
                }

                if (string.IsNullOrEmpty(ApplicationSettings.Instance.WirelessDevicePort))
                {
                    ApplicationSettings.Instance.WirelessDevicePort = "80";
                }

                if (ApplicationSettings.Instance.InchingDelay < 0)
                {
                    ApplicationSettings.Instance.InchingDelay = 0;
                }

                if (string.IsNullOrEmpty(ApplicationSettings.Instance.SerialPortSimplySwitchName))
                {
                    ApplicationSettings.Instance.SerialPortSimplySwitchName = "COM3";
                }

                if (ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate == default || ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate <= 0)
                {
                    ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate = 115200;
                }

                ApplicationSettings.Instance.IsServiceMode = Convert.ToBoolean(ConfigurationManager.AppSettings["IsServiceMode"]);

            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
        }

        private void Log(string msg, bool isError = false, bool isDebug = false)
        {
            //Console.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), msg));
            if (isError)
            {
                Gibraltar.Agent.Log.Warning("General", msg, "");
            }
            else if (isDebug)
            {
                Gibraltar.Agent.Log.Verbose("General", msg, "");
            }
            else
            {
                Gibraltar.Agent.Log.Information("General", msg, "");
            }
            try
            {
                lstLog.InvokeIfRequired(t => t.AddEntry(msg, isError));
            }
            catch (Exception e)
            {

                throw;
            }

        }
        #endregion

        #region Public Methods

        #endregion

        #region Eventhandlers

        #region Form events

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)

        {
            Cleanup();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var window = MessageBox.Show(
            //        "Are you sure you want to close the SimplySwitch Manager?",
            //        "Please confirm",
            //        MessageBoxButtons.YesNo);

            //e.Cancel = (window == DialogResult.No);

            try
            {
                isClosing = true;
                if (comThread != null)
                {
                    frmLoading.ShowLoadingForm();
                    comThread.Interrupt();
                    //comThread.Abort();
                    comThread.Join();
                    //comThread = null;
                    frmLoading.CloseForm();
                }
            }
            catch (Exception ex)
            {
                Log("Error closing form: " + ex.Message, true);
                e.Cancel = true;
            }
        }
        #endregion



        private void SerialInClient_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs args)
        {
            string statusString = args.Connected ? "successful" : "failed";
            Log("COM port connection status changed to '" + statusString + "'");
            picInSerialConnection.Image = args.Connected ? (Image)imgChecked.Clone() : (Image)imgUnchecked.Clone();
            DisplayNotificationBalloon("Club Manager Connection", "Connection " + statusString);

            if (messageBuffer != null)
            {
                messageBuffer.Clear();
            }

            messageBuffer = null;
            messageBuffer = new List<byte>();

            picScanResult.InvokeIfRequired(t => t.Visible = false);
        }

        private void SerialInClient_MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            try
            {
                //add data to the messagebuffer.            
                if (args.Data != null && args.Data.Count() > 0)
                {
                    Log("Data received. Looking for commands to process");

                    foreach (var item in args.Data)
                    {
                        messageBuffer.Add(item);
                    }

                    //put entire buffer into string variable, then we look for the EOF byte sequence i.e. char(10)char(13)char(10) i.e. "\n\r\n"
                    List<ISerialMessage> relayCommands = GetMessagesFromBuffer(ref messageBuffer, eofString);

                    if (relayCommands != null && relayCommands.Count() > 0)
                    {
                        picScanResult.InvokeIfRequired(t => t.Visible = false);

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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Log("Opening system configuration");

            DialogResult result = settingsForm.ShowDialog(this);
            
            if (result == DialogResult.OK)
            {
                InitializeSerialPort2Connection();

                VSPEManager v = new VSPEManager();
                if(!v.IsProcessRunning())
                {
                    MessageBox.Show("VSPE was closed in the background. The application needs to close. You will need to run the software again.");
                    Application.Exit();
                }
//                v.StartVSPE();

                Log("New configuration values loaded");

                string portName = ApplicationSettings.Instance.SerialPort2Name;
                int portBaudRate = ApplicationSettings.Instance.SerialPortPairBaudRate;

                //reconnect to the incoming serial port
                OpenComPort(serialPort2, portName, portBaudRate, picInSerialConnection);

                //setup for either wireless / wired comms to device
                //EnableDisableWirelessComms();

                InitializeSimplySwitchClient();
                var r = SimplySwitchConnect();
                if (r.ResponseCode == ResponseCode.Success)
                {
                    Log("SimplySwitch connected on " + simplySwitchClient.SerialPort);
                }
                else
                {
                    Log("Simply Switch failed to connect. " + r.Message, true);
                }
            }
            else
            {
                Log("Configuration values NOT changed");
            }
        }

        private void btnResetCOM_Click(object sender, EventArgs e)
        {
            string portName = ApplicationSettings.Instance.SerialPort2Name;
            int portBaudRate = ApplicationSettings.Instance.SerialPortPairBaudRate;
            OpenComPort(serialPort2, portName, portBaudRate, picInSerialConnection);
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            bool showSplash = Convert.ToBoolean(ConfigurationManager.AppSettings["IsServiceMode"]);
            if (showSplash)
            {
                frmSplash.CloseForm();
            }
            if (ConfigurationManager.AppSettings["IsDisplayFormOnStartup"].ToBool())
            {
                this.Activate();
                this.Focus();
            }

            //Log("Initialzing communication layer");
            InitSerialComms();

            ConfigureSystem();

            CheckSimplySwitchConnectionAndAutoDetect();
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
                //ExecuteWirelessCommand();
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

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayNotificationBalloon("header goes here", "this is a message");
        }

        private void btnViewLogs_Click(object sender, EventArgs e)
        {
            switch (formViewSize)
            {
                case FormViewSize.Small:
                    this.Size = sizeApplicationLogsOnly;

                    if (ApplicationSettings.Instance.IsTargetWireless)
                    {
                        txtUsbCommand.Focus();
                        txtUsbCommand.Select();
                    }

                    formViewSize = FormViewSize.ApplicationLogsOnly;
                    break;
                case FormViewSize.ApplicationLogsOnly:
                case FormViewSize.Full:
                    this.Size = sizeSmall;
                    btnViewLogs.Focus();
                    btnViewLogs.Select();

                    formViewSize = FormViewSize.Small;
                    break;
            }
        }

        private void btnUsbCommand_Click(object sender, EventArgs e)
        {
            try
            {
                Log("THIS FEATURE HAS BEEN DISABLED FOR NOW", true);
                return;

                //if (serialOutClient != null && serialOutClient.IsConnected && !string.IsNullOrEmpty(txtUsbCommand.Text.Trim()))
                //{
                //    byte[] btes = Encoding.ASCII.GetBytes(txtUsbCommand.Text.Trim() + "\r\n");
                //    Log("Sending USB command -> " + txtUsbCommand.Text.Trim());
                //    serialOutClient.SendMessage(btes);
                //}
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
            finally
            {
                txtUsbCommand.Text = "";
            }
        }

        private void btnUsbCommandOn_Click(object sender, EventArgs e)
        {

            var r = simplySwitchClient?.SActivate();
            if (r != null && r.ResponseCode != ResponseCode.Success)
            {
                Log(r.Message, true);
            }
        }

        private void btnUsbCommandOff_Click(object sender, EventArgs e)
        {
            var r = simplySwitchClient?.SRelease();
            if (r != null && r.ResponseCode != ResponseCode.Success)
            {
                Log(r.Message, true);
            }
        }

        //private void SerialInClient_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    Log(sender.ToString() + ": " + e.ToString());
        //    //throw new NotImplementedException();
        //}
        #endregion

        //private void SaveConfigurationValues()
        //{
        //    try
        //    {
        //        Log("Saving configuration");
        //        //ApplicationSettings.Instance.SerialPort2Name = txtSwOutPort.Text;
        //        //ApplicationSettings.Instance.SerialPortPairBaudRate = Convert.ToInt32(txtSerialPortPairBaudrate.Text);
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


        //private void ExecuteWiredCommand(string commandType, int portNumber)
        //{
        //    try
        //    {
        //        string port = ApplicationSettings.Instance.SerialPortPairBaudRate;

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


        #region SimplySwitch Removed Code

        //BlockingCollection<RelayCommand> commandQueue;
        //BackgroundWorker bwCommandProcessor;


        //private void InitializeSerialOutConnection()
        //{
        //    Log("Initializing serial OUT connection object...");
        //    if (serialOutClient == null)
        //    {
        //        serialOutClient = new SerialPortInput();
        //        serialOutClient.ConnectionStatusChanged += SerialOutClient_ConnectionStatusChanged;
        //        serialOutClient.MessageReceived += SerialOutClient_MessageReceived;

        //        usbMessageBuffer = new List<byte>();
        //    }
        //}

        //private void EnableDisableWirelessComms()
        //{
        //    try
        //    {
        //        bool isTargetWireless = ApplicationSettings.Instance.IsTargetWireless;

        //        if (!isTargetWireless)
        //        {
        //            OpenComPort(serialOutClient, ApplicationSettings.Instance.SerialPortSimplySwitchName, ApplicationSettings.Instance.SerialPortSimplySwitchBaudRate);
        //            picConnectionType.Image = (Image)imgUsbConnection.Clone();

        //            txtUsbCommand.Focus();
        //            txtUsbCommand.Select();

        //        }
        //        else
        //        {
        //            serialOutClient?.Disconnect();

        //            picConnectionType.Image = (Image)imgWifiConnection.Clone();

        //            btnViewLogs.Focus();
        //            btnViewLogs.Select();
        //        }

        //        grpUsbCommandPanel.Visible = !isTargetWireless;

        //        newLoadingForm?.Show();
        //        newLoadingForm?.Hide();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex.Message, true);
        //    }
        //}

        //private void StartCommandQueueConsumer()
        //{

        //    try
        //    {
        //        commandQueue = new BlockingCollection<RelayCommand>();
        //        bwCommandProcessor = new BackgroundWorker();
        //        bwCommandProcessor.DoWork += BwCommandProcessor_DoWork;
        //        bwCommandProcessor.RunWorkerAsync();
        //    }
        //    catch (Exception)
        //    {
        //        Log("**WARNING** Message processor failed to start! Please contact support!", true);
        //        throw;
        //    }
        //}

        //private void BwCommandProcessor_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        while (!this.Disposing && !isClosing)
        //        {
        //            if (!commandQueue.IsCompleted && commandQueue.Count() > 0)
        //            {
        //                RelayCommand commandEntry = commandQueue.Take();

        //                ProcessRelayCommand(commandEntry);

        //            }

        //            if (this.Disposing || isClosing)
        //                break;
        //            System.Threading.Thread.Sleep(10);
        //        }
        //    }
        //    catch (OperationCanceledException ex)
        //    {
        //        Log("EXCEPTION (BwCommandProcessor_DoWork): " + ex.Message + " STACKTRACE: " + ex.StackTrace, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log("EXCEPTION (BwCommandProcesso_DoWork): " + ex.Message + " STACKTRACE: " + ex.StackTrace, true);
        //        throw;
        //    }
        //}


        //private void ProcessRelayCommand(RelayCommand command)
        //{
        //    if (command.PreExecutionDelayMs > 0 && command.Command == RelayCommand.CommandType.Close)
        //    {
        //        Log(string.Format("Applying pre-activation delay of {0} second", command.PreExecutionDelayMs));
        //        ApplyPreActivationDelay(ApplicationSettings.Instance.InchingDelay);
        //    }

        //    if (ApplicationSettings.Instance.IsTargetWireless)
        //    {
        //        //pasting type comparison as reference
        //        //relayCommand.Command  == RelayCommand.CommandType.Open
        //        ExecuteWirelessCommand(command.Command);
        //    }
        //    else
        //    {
        //        ExecuteUsbCommand(command.Command);
        //    }
        //}

        //private void ExecuteUsbCommand(RelayCommand.CommandType commandType)
        //{
        //    try
        //    {
        //        Log("Executing USB command");

        //        string cmd = "";
        //        switch (commandType)
        //        {
        //            case RelayCommand.CommandType.Open:
        //                cmd = commandSerialRelayOpenStringFormat;
        //                Log("Sending OPEN command");
        //                break;
        //            case RelayCommand.CommandType.Close:
        //            default:
        //                cmd = commandSerialRelayCloseStringFormat;
        //                Log("Sending CLOSE command");
        //                break;
        //        }


        //        if (serialOutClient != null && serialOutClient.IsConnected)
        //        {


        //            byte[] bte = Encoding.ASCII.GetBytes(cmd);
        //            serialOutClient.SendMessage(bte);
        //        }
        //        else
        //        {
        //            Log("No connection to the controller exist. Please check the settings and make sure that the device is plugged in and switched on!", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex.Message, true);
        //    }
        //}

        //private void ExecuteWirelessCommand(RelayCommand.CommandType commandType)
        //{
        //    try
        //    {
        //        Log("Executing wireless command");
        //        string url = "";

        //        switch (commandType)
        //        {
        //            case RelayCommand.CommandType.Open:
        //                Log("Applying pre-activation delay, if applicable");
        //                ApplyPreActivationDelay(ApplicationSettings.Instance.InchingDelay);

        //                url = commandWifiRelayOpenStringFormat;
        //                Log("Sending OPEN command");
        //                break;
        //            case RelayCommand.CommandType.Close:
        //            default:
        //                url = commandWifiRelayCloseStringFormat;
        //                Log("Sending CLOSE command");
        //                break;
        //        }

        //        url = url.ToLower()
        //                    .Replace("{ip}", ApplicationSettings.Instance.WirelessDeviceIPAddress)
        //                    .Replace("{port}", ApplicationSettings.Instance.WirelessDevicePort);


        //        HttpPost(url);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex.Message, true);
        //    }
        //    finally
        //    {
        //        //give the loading form a chance to actually load properly before trying to close it, just in case the wifi command is super fast :P
        //        //Thread.Sleep(20);
        //        //frmLoading.CloseForm();
        //    }
        //}


        //private void SerialOutClient_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs args)
        //{
        //    string statusString = args.Connected ? "opened" : "closed"; //args.Connected.ToString();
        //    Log("USB port " + statusString);
        //    DisplayNotificationBalloon("Simply Switch", "USB connection " + statusString, args.Connected ? NotificationType.Info : NotificationType.Warning);

        //    if (!args.Connected)
        //    {
        //        rtbUsbOutput.BackColor = Color.White;
        //        rtbUsbOutput.ForeColor = Color.Black;
        //        txtUsbCommand.Select();
        //        txtUsbCommand.Focus();
        //        rtbUsbOutput.InvokeIfRequired(t => t.Clear());
        //    }
        //    else
        //    {
        //        grpUsbCommandPanel.InvokeIfRequired(t => t.Enabled = true);
        //        rtbUsbOutput.BackColor = Color.Black;
        //        rtbUsbOutput.ForeColor = Color.LimeGreen;
        //    }

        //    picScanResult.InvokeIfRequired(t => t.Visible = false);
        //}

        //private void SerialOutClient_MessageReceived(object sender, MessageReceivedEventArgs args)
        //{
        //    try
        //    {
        //        //add data to the messagebuffer.            
        //        if (args.Data != null && args.Data.Count() > 0)
        //        {
        //            string s = Encoding.UTF8.GetString(args.Data);

        //            rtbUsbOutput.InvokeIfRequired(r =>
        //            {
        //                r.AppendText(s);
        //                if (r.Lines.Length > 100)
        //                {
        //                    r.Clear();
        //                }
        //            });

        //            //foreach (var item in args.Data)
        //            //{
        //            //    messageBuffer.Add(item);
        //            //}
        //            //string msgEofString = "\r\n";
        //            ////put entire buffer into string variable, then we look for the EOF byte sequence i.e. char(10)char(13)char(10) i.e. "\n\r\n"
        //            //List<ISerialMessage> serialMessages = GetMessagesFromBuffer(ref usbMessageBuffer, msgEofString);

        //            //if (serialMessages != null && serialMessages.Count() > 0)
        //            //{                        
        //            //    foreach (var item in serialMessages)
        //            //    {
        //            //        rtbUsbOutput.AppendText(item.ToString());
        //            //        if(rtbUsbOutput.Lines.Length > 30)
        //            //        {
        //            //            rtbUsbOutput.Clear();
        //            //        }
        //            //    }
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex.Message, true);
        //    }
        //}
        #endregion

        private void btnRestart_Click(object sender, EventArgs e)
        {
            var r = simplySwitchClient?.Restart();
            Log(r.Message, r.ResponseCode != ResponseCode.Success);

        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            switch (formViewSize)
            {
                case FormViewSize.Small:
                case FormViewSize.ApplicationLogsOnly:
                    this.Size = sizeFull;

                    if (ApplicationSettings.Instance.IsTargetWireless)
                    {
                        txtUsbCommand.Focus();
                        txtUsbCommand.Select();
                    }

                    formViewSize = FormViewSize.ApplicationLogsOnly;
                    break;                
                case FormViewSize.Full:
                    this.Size = sizeSmall;
                    btnViewLogs.Focus();
                    btnViewLogs.Select();
                    formViewSize = FormViewSize.Small;
                    break;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
