using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK
{	
	public class Status
    {
        internal bool _isActivated;
        public bool IsActivated
        {
            get { return _isActivated; }
            internal set { _isActivated = value; }
        }

        internal string _firmwareVersion;
        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            internal set { _firmwareVersion = value; }
        }

        internal Status GetReturnStatusObject(bool isActivated = false, string firmwareVerson = "NAN") 
        {
            Status status = new Status();
            status.IsActivated = isActivated;
            status.FirmwareVersion = firmwareVerson;
            return status;
        }

        internal Status() 
        {
            _isActivated = false;
            _firmwareVersion = "NAN";
        }              
    }
}
