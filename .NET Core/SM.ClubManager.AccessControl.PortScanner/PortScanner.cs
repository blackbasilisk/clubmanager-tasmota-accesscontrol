using NLog.Fluent;
using SerialPortLib;
using SM.ClubManager.AccessControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SM.ClubManager.AccessControl.PortScanner
{
    
    public class Scanner : IDisposable
    {
        #pragma warning disable CS8618 
        public event EventHandler<string> OnLogEvent;
     
        SerialPortInput serialClient = new SerialPortInput();
        List<byte> messageBuffer =  new List<byte>();
        string msgReceivedFromSerialClient = "";
        private string _eofString ;
        int serialResponseWaitCounter = 0;
        bool isSerialMessageReceived = false;
        public string EOFString
        {
            get { return _eofString; }
            set { _eofString = value; }
        }

        #region CTor
        public Scanner(string eofString = "\r\n")
        {
            _eofString = eofString;


            //get list of ports
            //for each port, connect, send message, wait for response that matches expected response
        }

        #endregion
        //

        #region Private 

        #endregion
      
        public string FindDeviceByPort(List<string> listOfPorts, string messageToSend = "status\r\n", string expectedResponse = "CMD: status")
        {
           
            string foundPortName = "";
            //todo
            foreach (string port in listOfPorts)
            {
                try
                {
                    InitializeSerialPort();
                    //init serial
                    // open port
                    msgReceivedFromSerialClient = "";
                    OpenComPort(serialClient, port, 115200);

                    if (!serialClient.IsConnected)
                    {
                        continue;
                    }
                    byte[] msgToSend = Encoding.UTF8.GetBytes(messageToSend);

                    serialClient.SendMessage(msgToSend);

                    serialResponseWaitCounter = 0;
                    isSerialMessageReceived = false;

                    while (!isSerialMessageReceived && serialResponseWaitCounter < 100 && !msgReceivedFromSerialClient.EndsWith(expectedResponse))
                    {
                        serialResponseWaitCounter++;
                        Thread.Sleep(5);
                    }
                    serialClient.Disconnect();
                    if (isSerialMessageReceived)
                    {
                        if (msgReceivedFromSerialClient.EndsWith(expectedResponse))
                        {
                            Log("Found on " + port);
                            foundPortName = port;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log(ex.Message, true);
                }
                         
            }

            return foundPortName;
        }

        protected virtual void Log(string message,bool isError = false)
        {
            if(isError)
            {
                message = "Error: " + message;
            }

            OnLogEvent?.Invoke(this, message);
        }

        private void InitializeSerialPort()
        {
            try
            {
                Log("Initializing serial connection object...");

                if (serialClient != null)
                {

                    serialClient.Disconnect();
                    serialClient.MessageReceived -= SerialClient_MessageReceived;
                    serialClient.ConnectionStatusChanged -= SerialClient_ConnectionStatusChanged;
                    serialClient = null;
                }

                serialClient = new SerialPortInput();
                serialClient.ConnectionStatusChanged += SerialClient_ConnectionStatusChanged;
                serialClient.MessageReceived += SerialClient_MessageReceived;

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

        private void OpenComPort(SerialPortInput serialClient, string portName, int baudRate)
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

        private List<string> GetMessagesFromBuffer(ref List<byte> byteList, string eofString)
        {
            try
            {

                // string str = Encoding.Default.GetString(bytes);
                string strBuffer = Encoding.UTF8.GetString(byteList.ToArray());

                if (strBuffer.Contains(eofString))
                {
                    //find the location of the first _eofString so that we can cut the items before that out and then return the remaining bytes                 
                    var listOfMessages = strBuffer.Split(new string[] { eofString }, StringSplitOptions.RemoveEmptyEntries);

                    if (listOfMessages != null && listOfMessages.Count() > 0)
                    {
                        List<string> serialMessages = new List<string>();
                        //split the buffer into the various commands
                        foreach (var item in listOfMessages)
                        {                            
                            serialMessages.Add(item);
                        }

                        //we find the location of the last occurence of the _eofString
                        //deduct the index + _eofString.Length from the total byteList
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

        #region Eventhandlers

        private void SerialClient_ConnectionStatusChanged(object sender, ConnectionStatusChangedEventArgs args)
        {
            string statusString = args.Connected ? "successful" : "failed";
            Log("COM port connection status changed to '" + statusString + "'");


            if (messageBuffer != null)
            {
                messageBuffer.Clear();
            }

            messageBuffer = null;
            messageBuffer = new List<byte>();
            //msgReceivedFromSerialClient = "";           
        }

        private void SerialClient_MessageReceived(object sender, MessageReceivedEventArgs args)
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
                    List<string> messages = GetMessagesFromBuffer(ref messageBuffer, _eofString);
                    msgReceivedFromSerialClient = "";
                    foreach (var msg in messages)
                    {
                        //set message received data which will used to check against 
                        msgReceivedFromSerialClient = msg;
                        
                        isSerialMessageReceived = true;
                        //break because we only need to wait for the first message
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message, true);
            }
        }     
        #endregion

        #region Dispose methods
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {                    
                    // TODO: dispose managed state (managed objects)                 
                    if (serialClient != null)
                    {
                        serialClient.Disconnect();
                        serialClient.ConnectionStatusChanged -= SerialClient_ConnectionStatusChanged;
                        serialClient.MessageReceived -= SerialClient_MessageReceived;

                        serialClient = null;
                    }
                }

                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PortScanner()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

}
