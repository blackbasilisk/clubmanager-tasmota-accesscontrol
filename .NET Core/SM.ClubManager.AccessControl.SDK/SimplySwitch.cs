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

        public event EventHandler? OnSerialConnected;
        public event EventHandler? OnSerialDisconnected;
        public event EventHandler<SSLogMessage>? OnLogMessage;

        #endregion

        #region Fields 
        bool isDisposing = false;
        string serialComPort = "NAN";
        readonly int serialBaudRate = 115200;
        readonly string commandSerialRelayOpenStringFormat = "Power1 on\r\n";
        readonly string commandSerialRelayCloseStringFormat = "Power1 off\r\n";

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

        #endregion

        #region Constructor
        public SimplySwitch(string comPort, int baudRate)
        {
            serialComPort = comPort;
            serialBaudRate = baudRate;

            Initialize();
        }

        private void Initialize()
        {
            StartCommandQueueConsumer();
        }
        #endregion

        #region Public Methods
        public SSResponse Connect()
        {
            throw new NotImplementedException();
        }

        public SStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        public SSResponse Restart()
        {
            throw new NotImplementedException();
        }

        public SSResponse SClose(string ComPort, int BaudRate)
        {
            throw new NotImplementedException();
        }

        public SSResponse SClose()
        {
            throw new NotImplementedException();
        }

        public SSResponse SOpen(string ComPort, int BaudRate)
        {
            throw new NotImplementedException();
        }

        public SSResponse SOpen()
        {
            throw new NotImplementedException();
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

            OnSerialConnected = null;
            OnSerialDisconnected = null;
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

            OnLogMessage?.Invoke(this, SSLogMessage.GetNewLogMessage(msg, isError));
        }

        //private void EnableDisableSerialComms()
        //{
        //    try
        //    {
        //        OpenComPort(serialOutClient, serialComPort, serialBaudRate);                                                 
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex.Message, true);
        //    }
        //}

        private void SerialOutClient_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs args)
        {
            string statusString = args.Connected ? "opened" : "closed"; //args.Connected.ToString();

            if (args.Connected) {
                OnSerialConnected?.Invoke(this, new EventArgs());
            }
            else {
                OnSerialDisconnected?.Invoke(this, new EventArgs());  
            }
            Log("USB port " + statusString);

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
                    Console.Write(s);
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

        private void OpenComPort(SerialPortInput? serialClient, string portName, int baudRate)
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
                    if (serialClient == null)
                    {
                        throw new Exception("COM port has not been initialized");
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
                    case SSRelayCommand.CommandType.Open:
                        cmd = commandSerialRelayOpenStringFormat;
                        Log("Sending OPEN command");
                        break;
                    case SSRelayCommand.CommandType.Close:
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
        #endregion
    }

}