using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK
{
    internal interface ISimplySwitch : IDisposable
    {
        internal SSResponse Restart(); 

        internal SSResponse Connect();

        internal SSResponse SOpen(string ComPort, int BaudRate);

        internal SSResponse SOpen();

        internal SSResponse SClose(string ComPort, int BaudRate);

        internal SSResponse SClose();

        internal SStatus GetStatus();



    }
    
}
