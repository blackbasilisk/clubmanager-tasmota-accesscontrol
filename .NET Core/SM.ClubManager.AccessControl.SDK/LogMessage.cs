using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK
{	
	public class SSLogMessage
    {
        private bool _isError;

        public bool IsError
        {
            get { return _isError; }
            internal set { _isError = value; }
        }

        private string _message = "";

        public string Message
        {
            get { return _message; }
            internal set { _message = value; }
        }

        internal SSLogMessage() { }

        internal static SSLogMessage GetNewLogMessage(string message, bool isError = false) 
        {
            return new SSLogMessage() { Message = message, IsError = isError };
        }
    }
}
