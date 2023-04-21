using SerialPortLib;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Transactions;

namespace SM.ClubManager.AccessControl.SDK
{
    public class SimplySwitch : ISimplySwitch
    {

        #region Events

        public event EventHandler? OnConnected;
        public event EventHandler? OnDisconnected;
        public event EventHandler<SSLogMessage>? OnLogMessage;

        #endregion

        #region Fields 
        bool isDisposing = false;

        readonly string tasmotaCommandOpen = "Power1 on\r\n";
        readonly string tasmotaCommandClose = "Power1 off\r\n";
        readonly string tasmotaCommandRestart = "Restart 1\r\n";

        //<add key = "Cmd.Format.Serial.RelayClose" value="Power1 on&#xD;&#xA;" />       
        //<add key = "Cmd.Format.Serial.RelayOpen" value="Power1 off&#xD;&#xA;" />

        BlockingCollection<SSRelayCommand>? commandQueue;
        BackgroundWorker? bwCommandProcessor;
        SerialPortInput? serialOutClient = new();
        List<byte>? usbMessageBuffer = null;
        #endregion

        #region Properties        
        private ushort _switchDelay;

        public ushort SwitchDelay
        {
            get { return _switchDelay; }
            internal set { _switchDelay = value; }
        }

        private bool _isConnected;

        public bool IsConnected
        {
            get { return _isConnected; }
            internal set { _isConnected = value; }
        }

        private string _serialPort;

        public string SerialPort
        {
            get { return _serialPort; }
            set { _serialPort = value; }
        }


        private int _serialBaudRate;

        public int SerialBaudRate
        {
            get { return _serialBaudRate; }
            set { _serialBaudRate = value; }
        }

        #endregion

        #region Constructor
        public SimplySwitch(string comPort, int baudRate = 115200)
        {
            _serialPort = comPort;
            _serialBaudRate = baudRate;

            Initialize();
        }

        public SimplySwitch()
        {
            Initialize();
        }

            private void Initialize()
        {           
            StartCommandQueueConsumer();

            if(serialOutClient == null)
            {
                serialOutClient = new SerialPortInput();
            }
            serialOutClient.ConnectionStatusChanged += SerialOutClient_ConnectionStatusChanged;
            serialOutClient.MessageReceived += SerialOutClient_MessageReceived;            
        }

     
        #endregion

        #region Public Methods
        public SSResponse SOpen(ushort preExecutionDelayMs = 0)
        {
            try
            {
                //The command is close because it closes the relay, but from the consumer perspective it 'opens' the gate 
                SSRelayCommand command = SSRelayCommand.Create(SSRelayCommand.CommandType.Close, preExecutionDelayMs);
                commandQueue?.Add(command);
                return SSResponse.Create(ResponseCode.Success);
            }
            catch (Exception)
            {
                Log("Error adding command close relay command to command queue", true);
                return SSResponse.Create(ResponseCode.Unknown);                
            }                    
        }

        public SSResponse SClose(ushort preExecutionDelayMs = 0)
        {
            try
            {
                //The command is open because it opens the relay, but from the consumer perspective it 'closes' the gate 
                SSRelayCommand command = SSRelayCommand.Create(SSRelayCommand.CommandType.Open, preExecutionDelayMs);
                commandQueue?.Add(command);
                return SSResponse.Create(ResponseCode.Success);
            }
            catch (Exception)
            {
                Log("Error adding command close relay command to command queue", true);
                return SSResponse.Create(ResponseCode.Unknown);
            }
        }

        //public SStatus GetStatus()
        //{
        //    throw new NotImplementedException();
        //}

        public SSResponse Restart()
        {
            try
            {              
                SSRelayCommand command = SSRelayCommand.Create(SSRelayCommand.CommandType.Restart);
                commandQueue?.Add(command);
                return SSResponse.Create(ResponseCode.Success);
            }
            catch (Exception)
            {
                Log("Error adding restart command to command queue", true);
                return SSResponse.Create(ResponseCode.Unknown);
            }
        }
      
        public SSResponse SDisconnect()
        {
            try
            {
                Log("Closing port " + _serialPort);
                EnableDisableConnection(false);
                return SSResponse.Create(ResponseCode.Success, "");
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);                
                return SSResponse.Create(ResponseCode.Unknown, "Error when trying to close serial port");
            }
        }

        public SSResponse SConnect()
        {
            try
            {
                if(_serialPort == null || _serialPort == "")
                {
                    throw new InvalidOperationException("SerialPort has not been set");
                }
                if (_serialBaudRate <= 0)
                {
                    throw new InvalidOperationException("BaudRate has not been set");
                }
                Log("Opening port " + _serialPort);
                EnableDisableConnection(true);
                return SSResponse.Create(ResponseCode.Success, "");
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
                EnableDisableConnection(false);
                return SSResponse.Create(ResponseCode.InitializeError, "Failed to open serial port");
            }
        }
      
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            try
            {
                isDisposing = true;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            { 
                isDisposing = false; 
            }    
           
            Cleanup();
        }

        #endregion

        #region Internal Methods       

        private void Cleanup()
        {
            isDisposing = true;
            if(commandQueue != null)
            {
                commandQueue?.CompleteAdding();
                var items = commandQueue?.Take(commandQueue.Count);
                if(items != null)
                {
                    foreach (var item in items)
                    {
                        item.Dispose();
                    }
                    items = null;
                }              
            }
            
            if (serialOutClient != null)
            {
                serialOutClient.Disconnect();                
                serialOutClient.ConnectionStatusChanged -= SerialOutClient_ConnectionStatusChanged;
                serialOutClient.MessageReceived -= SerialOutClient_MessageReceived;
                serialOutClient = null;
            }

            OnConnected = null;
            OnDisconnected = null;
            OnLogMessage = null;

            commandQueue?.Dispose();
            commandQueue = null;
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

            OnLogMessage?.Invoke(this, SSLogMessage.GetNewLogMessage(msg, isError, isDebug));
        }

        private void SerialOutClient_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs args)
        {
            string statusString = args.Connected ? "opened" : "closed"; //args.Connected.ToString();

            if (args.Connected) {
                OnConnected?.Invoke(this, new EventArgs());
            }
            else {
                OnDisconnected?.Invoke(this, new EventArgs());  
            }
            Log("Port " + statusString);

            //Set internal connection status var based on statusString
            _isConnected = args.Connected;
            
            usbMessageBuffer?.Clear();           

            usbMessageBuffer = null;
            usbMessageBuffer = new List<byte>();    
        }

        private void SerialOutClient_MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            try
            {
                //add data to the messagebuffer.            
                if (args.Data != null && args.Data.Length > 0)
                {
                    string s = Encoding.UTF8.GetString(args.Data);
                    Log(s, false, true);                    
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
        }

        private void StartCommandQueueConsumer()
        {
            try
            {
                commandQueue = new BlockingCollection<SSRelayCommand>();
                bwCommandProcessor = new BackgroundWorker();
                bwCommandProcessor.DoWork += BwCommandProcessor_DoWork;
                bwCommandProcessor.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Log("**WARNING** Message processor failed to start! Please contact support!", true);
                throw;
            }
        }

        private void BwCommandProcessor_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                while (!isDisposing)
                {
                    if (commandQueue != null && !commandQueue.IsCompleted && commandQueue.Count > 0)
                    {
                        SSRelayCommand commandEntry = commandQueue.Take();

                        ProcessRelayCommand(commandEntry);

                    }

                    if (isDisposing)
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

        private void ProcessRelayCommand(SSRelayCommand command)
        {
            if (command.PreExecutionDelayMs > 0 && command.Command == SSRelayCommand.CommandType.Close)
            {
                Log(string.Format("Applying pre-activation delay of {0} second", command.PreExecutionDelayMs));
                ApplyPreActivationDelay(_switchDelay);
            }

                ExecuteUsbCommand(command.Command);            
        }

        private void EnableDisableConnection(bool isEnable = true)
        {
            try
            {
                if (isEnable)
                {
                    OpenComPort(serialOutClient, _serialPort, _serialBaudRate);
                }
                else
                {
                    serialOutClient?.Disconnect();
                }         
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
                throw;
            }
        }
    
        private void OpenComPort(SerialPortInput? serialClient, string portName, int baudRate)
        {
            try
            {               
                Log(string.Format("Opening port {0}...", portName));
       
                try
                {
                    if (serialClient == null)
                    {
                        throw new Exception("COM port object has not been initialized");
                    }
                    serialClient.Disconnect();

                    serialClient.SetPort(portName: portName, baudRate: baudRate);

                    serialClient.Connect();

                    if (serialClient != null && serialClient.IsConnected)
                    {
                        Log(string.Format("Port {0} opened OK", portName));                        
                    }
                    else
                    {                                               
                        throw new Exception(string.Format("ERROR opening port {0}. Check serial port parameters.", portName));
                    }
                }
                catch (Exception serialEx)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }

            return;
        }

        private void ApplyPreActivationDelay(ushort delay)
        {
            try
            {
                //ushort displayDelay = delay;
                ushort delayInMilliseconds = (ushort)(delay * 1000);

                if (delay > 0)
                {             
                    //Add in an additional 100ms delay due to Windows Forms loading processes that might cause issues for the consumer                    
                    if (delay < 100)
                    {
                        delay += 100;
                    }

                    System.Threading.Thread.Sleep(delay);                    
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
        }

        private void ExecuteUsbCommand(SSRelayCommand.CommandType commandType)
        {
            try
            {
                Log("Executing USB command");

                string cmd = "";
                switch (commandType)
                {
                    case SSRelayCommand.CommandType.Restart:
                        cmd = tasmotaCommandRestart;
                        Log("Sending RESTART command");
                        break;
                    case SSRelayCommand.CommandType.Open:
                        cmd = tasmotaCommandOpen;
                        Log("Sending OPEN command");
                        break;
                    case SSRelayCommand.CommandType.Close:
                    default:
                        cmd = tasmotaCommandClose;
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
        #endregion
    }

}