using SM.ClubManager.AccessControl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl
{
    public class RelayCommand : ISerialMessage
    {
        #region Types
        public enum CommandType
        {
            Close,
            Open
        }

        #endregion

        #region Properties
        private CommandType _command;

        public CommandType Command
        {
            get { return _command; }
            set { _command = value; }
        }

        private int _portNo;

        public int PortNo
        {
            get { return _portNo; }
            set { _portNo = value; }
        }
        #endregion

        public RelayCommand()
        {

        }      
        
        public ISerialMessage Create(string message)
        {
            try
            {
                RelayCommand command = new RelayCommand();

                if (string.IsNullOrEmpty(message))
                {
                    throw new Exception("Cannot generate command from an empty string");
                }
               
                switch (message.Substring(0,1).ToUpper())
                {
                    case "N":
                        command.Command = CommandType.Close;
                        break;
                    case "F":
                        command.Command = CommandType.Open;
                        break;
                    default:
                        throw new Exception("Invalid command type received. Only N and F commands are catered for. Please contact support.");                        
                }

                string sPortNo = message.Substring(1, message.Length - 1);

                if (int.TryParse(sPortNo, out int portNo))
                {
                    command.PortNo = portNo;
                }
                else
                {
                    throw new Exception("Command type OK, but cannot parse the port number");
                }

                return command;
            }
            catch (Exception)
            {
                throw;
            }           
        }
    }   
}
