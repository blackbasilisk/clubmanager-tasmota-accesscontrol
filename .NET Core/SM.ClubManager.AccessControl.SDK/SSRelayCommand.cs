using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK
{
    public class SSRelayCommand
    {
        #region Types
        public enum CommandType
        {
            Close,
            Open,
            Restart,
            GetStatus
        }

        #endregion

        #region Properties
        private ushort _preExecutionDelayMs;

        public ushort PreExecutionDelayMs
        {
            get { return _preExecutionDelayMs; }
            set { _preExecutionDelayMs = value; }
        }


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

        public SSRelayCommand()
        {

        }

        public static SSRelayCommand Create(CommandType commandType, ushort preExecutionDelayMs = 0)
        {
            try
            {
                SSRelayCommand command = new();
                command.Command = commandType;                 
                command.PreExecutionDelayMs = preExecutionDelayMs;               

                return command;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _portNo = default;
            _command = default;
            _preExecutionDelayMs = default;
        }
    }   
}
