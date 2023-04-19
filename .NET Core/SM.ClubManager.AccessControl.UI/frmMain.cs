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

    public partial class frmMain : Form
    {
        Size smallSize = new Size(279, 307);
        Size largeSize = new Size(1190, 630);
        bool isSmallSizeMode = true;
        bool isClosing = false;

        string commandWifiRelayOpenStringFormat = "";
        string commandWifiRelayCloseStringFormat = "";
        string commandSerialRelayOpenStringFormat = "";
        string commandSerialRelayCloseStringFormat = "";

        //SimpleSerialClient serialInClient = null;
        SerialPortInput serialInClient = null;//new SerialPortInput();
        SerialPortInput serialOutClient = null;//new SerialPortInput();
        //SerialPortStream serialInClient = new SerialPortStream();
        frmSettings settingsForm = new frmSettings();
        frmNewLoading newLoadingForm = new frmNewLoading();
        List<byte> messageBuffer = null;
        List<byte> usbMessageBuffer = null;
        System.Threading.Thread comThread;
        string eofString = "\n\r\n";
        Image imgUnchecked;
        Image imgChecked;
        Image imgUsbConnection;
        Image imgWifiConnection;
        Icon notificationWarningIcon;
        Icon notificationInformationIcon;
        Image imgInfo;

        BlockingCollection<RelayCommand> commandQueue;
        BackgroundWorker bwCommandProcessor;

        public frmMain()
        {
            InitializeComponent();

            Initialize();
        }

        #region Init Methods

        private void Initialize()
        {
            isSmallSizeMode = true;
            this.Size = smallSize;

            Log("Initializing user interface");
            this.ShowInTaskbar = ConfigurationManager.AppSettings["IsDisplayInTaskBar"].ToBool();

            //Log("Intializing database");
            //System.Data.Entity.Database.SetInitializer(new DatabaseInitializer());

            Log("Preloading assets");
            PreloadAssets();

            Log("Assigning resources");
            AssignResources();

            Log("Initializing configuration...");
            SetDefaults();


            commandWifiRelayOpenStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Wifi.RelayOpen"];
            commandWifiRelayCloseStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Wifi.RelayClose"];
            commandSerialRelayOpenStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Serial.RelayOpen"];
            commandSerialRelayCloseStringFormat = ConfigurationManager.AppSettings["Cmd.Format.Serial.RelayClose"];

            newLoadingForm.StartPosition = FormStartPosition.Manual;
            newLoadingForm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (newLoadingForm.Width / 2), Screen.PrimaryScreen.WorkingArea.Height - newLoadingForm.Height - 50);

            StartCommandQueueConsumer();

            Log("Startup completed");
        }


        private void AssignResources()
        {

        }

        private void ConfigureSystem()
        {
            EnableDisableWirelessComms();
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

        private void InitSerialComms()
        {

            InitializeSerialInConnection();

            string portName = ApplicationSettings.Instance.SerialInPort;
            int portBaudRate = ApplicationSettings.Instance.SerialInBaudRate;

            OpenComPort(serialInClient, portName, portBaudRate, picInSerialConnection);
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

        private void StartCommandQueueConsumer()
        {

            try
            {
                commandQueue = new BlockingCollection<RelayCommand>();
                bwCommandProcessor = new BackgroundWorker();
                bwCommandProcessor.DoWork += BwCommandProcessor_DoWork;
                bwCommandProcessor.RunWorkerAsync();
            }
            catch (Exception)
            {
                Log("**WARNING** Message processor failed to start! Please contact support!", true);
                throw;
            }
        }

        private void BwCommandProcessor_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (!this.Disposing && !isClosing)
                {
                    if (!commandQueue.IsCompleted && commandQueue.Count() > 0)
                    {
                        RelayCommand commandEntry = commandQueue.Take();

                        ProcessRelayCommand(commandEntry);

                    }

                    if (this.Disposing || isClosing)
                        break;
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (OperationCanceledException ex)
            {
                Log("EXCEPTION (BwCommandProcessor_DoWork): " + ex.Message + " STACKTRACE: " + ex.StackTrace, true);
            }
            catch (Exception ex)
            {
                Log("EXCEPTION (BwCommandProcesso_DoWork): " + ex.Message + " STACKTRACE: " + ex.StackTrace, true);
                throw;
            }
        }

        private void ProcessRelayCommand(RelayCommand command)
        {
            if (command.PreExecutionDelayMs > 0 && command.Command == RelayCommand.CommandType.Close)
            {
                Log(string.Format("Applying pre-activation delay of {0} second", command.PreExecutionDelayMs));
                ApplyPreActivationDelay(ApplicationSettings.Instance.InchingDelay);
            }

            if (ApplicationSettings.Instance.IsTargetWireless)
            {
                //pasting type comparison as reference
                //relayCommand.Command  == RelayCommand.CommandType.Open
                ExecuteWirelessCommand(command.Command);
            }
            else
            {
                ExecuteUsbCommand(command.Command);
            }
        }
        #endregion

        #region Private methods
        //////[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //////private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        //////private const int WM_VSCROLL = 277;
        //////private const int SB_PAGEBOTTOM = 7;

        //////internal static void ScrollToBottom(RichTextBox richTextBox)
        //////{
        //////    SendMessage(richTextBox.Handle, WM_VSCROLL, (IntPtr)SB_PAGEBOTTOM, IntPtr.Zero);
        //////    richTextBox.SelectionStart = richTextBox.Text.Length;
        //////}
        private void DisplayNotificationBalloon(string header, string message, NotificationType notificationType = NotificationType.Info)
        {
            Icon icon = this.Icon;
            ToolTipIcon ttIcon = ToolTipIcon.Info;

            switch (notificationType)
            {
                case NotificationType.Error:
                    //icon = notificationWarningIcon;
                    ttIcon = ToolTipIcon.Error;
                    break;
                case NotificationType.Info:
                    //icon = notificationInformationIcon;
                    ttIcon = ToolTipIcon.Info;
                    break;
                default:
                    ttIcon = ToolTipIcon.None;
                    break;
            }

            NotifyIcon notifyIcon = new NotifyIcon
            {
                Visible = true,
                Icon = icon,
                BalloonTipIcon = ttIcon,
                Text = ""
            };
            if (header != null)
            {
                notifyIcon.BalloonTipTitle = header;
            }
            if (message != null)
            {
                notifyIcon.BalloonTipText = message;
            }

            notifyIcon.BalloonTipClosed += (sender, args) => DisposeNotification(notifyIcon);
            notifyIcon.BalloonTipClicked += (sender, args) => DisposeNotification(notifyIcon);
            notifyIcon.ShowBalloonTip(0);
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

                //int count = 0;
                //bool isConnectionOk = false;
                //while (!isConnectionOk && count < 5)
                //{
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
                commandQueue.Add(relayCommand);
                //Adding delay to allow the 'loading' image to be displayed so user can see something happened. serves no other purpose
                Thread.Sleep(500);

            }
            catch (Exception)
            {
                throw;
            }

            Log("Relay command processing completed");
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

        private void EnableDisableWirelessComms()
        {
            try
            {
                bool isTargetWireless = ApplicationSettings.Instance.IsTargetWireless;

                if (!isTargetWireless)
                {
                    OpenComPort(serialOutClient, ApplicationSettings.Instance.SerialOutPort, ApplicationSettings.Instance.SerialOutBaudRate);
                    picConnectionType.Image = (Image)imgUsbConnection.Clone();

                    txtUsbCommand.Focus();
                    txtUsbCommand.Select();

                }
                else
                {
                    serialOutClient?.Disconnect();

                    picConnectionType.Image = (Image)imgWifiConnection.Clone();

                    btnViewLogs.Focus();
                    btnViewLogs.Select();
                }

                grpUsbCommandPanel.Visible = !isTargetWireless;

                newLoadingForm?.Show();
                newLoadingForm?.Hide();
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
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
            commandQueue.CompleteAdding();
            var items = commandQueue.Take(commandQueue.Count);
            foreach (var item in items)
            {
                item.Dispose();
            }
            items = null;

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

            if (serialInClient != null)
            {
                serialInClient.Disconnect();
                serialInClient.ConnectionStatusChanged -= SerialInClient_ConnectionStatusChanged;
                serialInClient.MessageReceived -= SerialInClient_MessageReceived;

                serialInClient = null;
            }

            if (serialOutClient != null)
            {
                serialOutClient.Disconnect();
                serialOutClient.ConnectionStatusChanged -= SerialOutClient_ConnectionStatusChanged;
                serialOutClient.MessageReceived -= SerialOutClient_MessageReceived;
                serialOutClient = null;
            }

            lstLog.Dispose();
            lstLog = null;
            commandQueue.Dispose();
            commandQueue = null;
        }

        private void ExecuteWirelessCommand(RelayCommand.CommandType commandType)
        {
            try
            {
                Log("Executing wireless command");
                string url = "";

                switch (commandType)
                {
                    case RelayCommand.CommandType.Open:
                        Log("Applying pre-activation delay, if applicable");
                        ApplyPreActivationDelay(ApplicationSettings.Instance.InchingDelay);

                        url = commandWifiRelayOpenStringFormat;
                        Log("Sending OPEN command");
                        break;
                    case RelayCommand.CommandType.Close:
                    default:
                        url = commandWifiRelayCloseStringFormat;
                        Log("Sending CLOSE command");
                        break;
                }

                url = url.ToLower()
                            .Replace("{ip}", ApplicationSettings.Instance.WirelessDeviceIPAddress)
                            .Replace("{port}", ApplicationSettings.Instance.WirelessDevicePort);


                HttpPost(url);
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
            finally
            {
                //give the loading form a chance to actually load properly before trying to close it, just in case the wifi command is super fast :P
                //Thread.Sleep(20);
                //frmLoading.CloseForm();
            }
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

        private void ExecuteUsbCommand(RelayCommand.CommandType commandType)
        {
            try
            {
                Log("Executing USB command");

                string cmd = "";
                switch (commandType)
                {
                    case RelayCommand.CommandType.Open:
                        cmd = commandSerialRelayOpenStringFormat;
                        Log("Sending OPEN command");
                        break;
                    case RelayCommand.CommandType.Close:
                    default:
                        cmd = commandSerialRelayCloseStringFormat;
                        Log("Sending CLOSE command");
                        break;
                }


                if (serialOutClient != null && serialOutClient.IsConnected)
                {


                    byte[] bte = Encoding.ASCII.GetBytes(cmd);
                    serialOutClient.SendMessage(bte);
                }
                else
                {
                    Log("No connection to the controller exist. Please check the settings and make sure that the device is plugged in and switched on!", true);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
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

        private void InitializeSerialInConnection()
        {
            try
            {
                Log("Initializing serial connection objects...");

                if (serialInClient == null)
                {
                    serialInClient = new SerialPortInput();
                    serialInClient.ConnectionStatusChanged += SerialInClient_ConnectionStatusChanged;
                    serialInClient.MessageReceived += SerialInClient_MessageReceived;

                    messageBuffer = new List<byte>();
                }

                if (serialOutClient == null)
                {
                    serialOutClient = new SerialPortInput();
                    serialOutClient.ConnectionStatusChanged += SerialOutClient_ConnectionStatusChanged;
                    serialOutClient.MessageReceived += SerialOutClient_MessageReceived;

                    usbMessageBuffer = new List<byte>();
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

                if (ApplicationSettings.Instance.SerialInBaudRate == default || ApplicationSettings.Instance.SerialInBaudRate <= 0)
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

                if (ApplicationSettings.Instance.InchingDelay < 0)
                {
                    ApplicationSettings.Instance.InchingDelay = 0;
                }

                if (string.IsNullOrEmpty(ApplicationSettings.Instance.SerialOutPort))
                {
                    ApplicationSettings.Instance.SerialOutPort = "COM3";
                }

                if (ApplicationSettings.Instance.SerialOutBaudRate == default || ApplicationSettings.Instance.SerialOutBaudRate <= 0)
                {
                    ApplicationSettings.Instance.SerialOutBaudRate = 115200;
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
            var window = MessageBox.Show(
                    "Are you sure you want to close the SimplySwitch Manager?",
                    "Please confirm",
                    MessageBoxButtons.YesNo);

            e.Cancel = (window == DialogResult.No);

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

        private void SerialOutClient_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs args)
        {
            string statusString = args.Connected ? "opened" : "closed"; //args.Connected.ToString();
            Log("USB port " + statusString);
            DisplayNotificationBalloon("Simply Switch", "USB connection " + statusString, args.Connected ? NotificationType.Info : NotificationType.Warning);

            if (!args.Connected)
            {
                rtbUsbOutput.BackColor = Color.White;
                rtbUsbOutput.ForeColor = Color.Black;
                txtUsbCommand.Select();
                txtUsbCommand.Focus();
                rtbUsbOutput.InvokeIfRequired(t => t.Clear());
            }
            else
            {
                grpUsbCommandPanel.InvokeIfRequired(t => t.Enabled = true);
                rtbUsbOutput.BackColor = Color.Black;
                rtbUsbOutput.ForeColor = Color.LimeGreen;
            }


            usbMessageBuffer?.Clear();


            usbMessageBuffer = null;
            usbMessageBuffer = new List<byte>();

            picScanResult.InvokeIfRequired(t => t.Visible = false);
        }

        private void SerialOutClient_MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            try
            {
                //add data to the messagebuffer.            
                if (args.Data != null && args.Data.Count() > 0)
                {
                    string s = Encoding.UTF8.GetString(args.Data);

                    rtbUsbOutput.InvokeIfRequired(r =>
                    {
                        r.AppendText(s);
                        if (r.Lines.Length > 100)
                        {
                            r.Clear();
                        }
                    });

                    //foreach (var item in args.Data)
                    //{
                    //    messageBuffer.Add(item);
                    //}
                    //string msgEofString = "\r\n";
                    ////put entire buffer into string variable, then we look for the EOF byte sequence i.e. char(10)char(13)char(10) i.e. "\n\r\n"
                    //List<ISerialMessage> serialMessages = GetMessagesFromBuffer(ref usbMessageBuffer, msgEofString);

                    //if (serialMessages != null && serialMessages.Count() > 0)
                    //{                        
                    //    foreach (var item in serialMessages)
                    //    {
                    //        rtbUsbOutput.AppendText(item.ToString());
                    //        if(rtbUsbOutput.Lines.Length > 30)
                    //        {
                    //            rtbUsbOutput.Clear();
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
        }

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
            DialogResult result = settingsForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                Log("New configuration values loaded");

                string portName = ApplicationSettings.Instance.SerialInPort;
                int portBaudRate = ApplicationSettings.Instance.SerialInBaudRate;

                //reconnect to the incoming serial port
                OpenComPort(serialInClient, portName, portBaudRate, picInSerialConnection);

                //setup for either wireless / wired comms to device
                EnableDisableWirelessComms();
            }
            else
            {
                Log("Configuration values NOT changed");
            }
        }

        private void btnResetCOM_Click(object sender, EventArgs e)
        {
            string portName = ApplicationSettings.Instance.SerialInPort;
            int portBaudRate = ApplicationSettings.Instance.SerialInBaudRate;
            OpenComPort(serialInClient, portName, portBaudRate, picInSerialConnection);
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
            isSmallSizeMode = !isSmallSizeMode;
            if (isSmallSizeMode)
            {
                this.Size = smallSize;
                btnViewLogs.Focus();
                btnViewLogs.Select();
            }
            else
            {
                this.Size = largeSize;
                if (ApplicationSettings.Instance.IsTargetWireless)
                {
                    txtUsbCommand.Focus();
                    txtUsbCommand.Select();
                }
            }
        }

        private void btnUsbCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialOutClient != null && serialOutClient.IsConnected && !string.IsNullOrEmpty(txtUsbCommand.Text.Trim()))
                {
                    byte[] btes = Encoding.ASCII.GetBytes(txtUsbCommand.Text.Trim() + "\r\n");
                    Log("Sending USB command -> " + txtUsbCommand.Text.Trim());
                    serialOutClient.SendMessage(btes);
                }
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
            string command = "POWER1 ON";
            Log("Manual USB command executed");
            txtUsbCommand.Text = command;
            btnUsbCommand.PerformClick();
            //btnUsbCommand_Click(null, null);
        }

        private void btnUsbCommandOff_Click(object sender, EventArgs e)
        {
            Log("Manual USB command executed");
            string command = "POWER1 OFF";
            txtUsbCommand.Text = command;
            btnUsbCommand.PerformClick();
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
    }
}
