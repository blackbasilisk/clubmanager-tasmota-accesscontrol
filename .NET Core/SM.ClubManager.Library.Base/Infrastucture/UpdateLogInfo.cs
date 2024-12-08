using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.Library.Base.Infrastructure
{
    public class UpdateLogInfo
    {
        private string _caption;
        public string Caption
        {
            get { return _caption; }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
        }

        private Exception _exception;
        public Exception Exception
        {
            get { return _exception; }
        }

        private bool _isErrorMessage;
        public bool IsErrorMessage
        {
            get { return _isErrorMessage; }
        }

        private bool _isUserMessage;
        public bool IsUserMessage
        {
            get { return _isUserMessage; }
        }

        private bool _isDebugMessage;
        public bool IsDebugMessage
        {
            get { return _isDebugMessage; }
        }
        private bool _isCommunicationDebugMessage;

        public bool IsCommunicationDebugMessage
        {
            get { return _isCommunicationDebugMessage; }
        }

        private string _category;

        public string Category
        {
            get { return _category; }
        }


        //(string str, bool IsUpdateUserMessage = false, bool isErrorMessage = false, bool IsDebugMessage = false, bool IsPLCDebugMessage = false)                
        public UpdateLogInfo(string category, string caption, string description = "", bool isErrorMessage = false, bool isDebugMessage = false, bool isCommunicationDebugMessage = false, Exception exception = null)
        {
            _category = category;
            _caption = caption;
            _description = description;
            _isErrorMessage = isErrorMessage;
            _isDebugMessage = isDebugMessage;
            _isCommunicationDebugMessage = isCommunicationDebugMessage;
            _exception = exception;
        }
    }
}
