using SM.ClubManager.AccessControl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl
{
    public class UsbMessage : ISerialMessage
    { 

        #region Properties
        private string messageString;

        public string MessageString
        {
            get { return messageString; }
            set { messageString = value; }
        }

        private int _portNo;

        public int PortNo
        {
            get { return _portNo; }
            set { _portNo = value; }
        }
        #endregion


        public override string ToString()
        {
            return messageString;
        }

        public UsbMessage()
        {

        }      
        
        public ISerialMessage Create(string messageString)
        {
            try
            {
                UsbMessage message = new UsbMessage();

                if (string.IsNullOrEmpty(messageString))
                {
                    throw new Exception("Cannot generate message from an empty string");
                }

                message.MessageString = messageString;

                return message;
            }
            catch (Exception)
            {
                throw;
            }           
        }
    }   
}
