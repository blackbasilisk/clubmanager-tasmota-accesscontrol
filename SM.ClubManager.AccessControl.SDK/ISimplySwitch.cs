using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK
{
    internal interface ISimplySwitch : IDisposable
    {
        internal SResponse Restart(); 

        internal SResponse Connect();

        internal SResponse SOpen(string ComPort, int BaudRate);

        internal SResponse SOpen();

        internal SResponse SClose(string ComPort, int BaudRate);

        internal SResponse SClose();

        internal Status GetStatus();



    }
    
}
