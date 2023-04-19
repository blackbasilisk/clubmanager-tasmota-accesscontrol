using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK
{
    public interface ISimplySwitch : IDisposable
    {
        public SSResponse Restart();

        public SSResponse SOpen(ushort preExecutionDelayMs = 0);

        public SSResponse SClose(ushort preExecutionDelayMs = 0);

        public SSResponse SConnect();

        public SSResponse SDisconnect();

        //public SStatus GetStatus();



    }
    
}
