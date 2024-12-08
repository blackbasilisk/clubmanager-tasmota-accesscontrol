using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.Library.Base.Infrastructure
{
    public class UpdateLogInfoEventArgs : EventArgs
    {
        private UpdateLogInfo _updateLogInfo;

        public UpdateLogInfo UpdateLogInfo
        {
            get { return _updateLogInfo; }
            set { _updateLogInfo = value; }
        }

        public UpdateLogInfoEventArgs(UpdateLogInfo updateLogInfo)
        {

        }

        public UpdateLogInfoEventArgs(string category, string caption, string description = "", bool isErrorMessage = false, bool isDebugMessage = false, bool isCommunicationDebugMessage = false, Exception exception = null)
        {
            _updateLogInfo = new UpdateLogInfo(category, caption, description, isErrorMessage, isDebugMessage, isCommunicationDebugMessage, exception);
        }
    }
}
