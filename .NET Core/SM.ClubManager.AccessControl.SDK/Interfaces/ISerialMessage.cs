using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.SDK.Interfaces
{
    public interface ISSerialMessage : IDisposable
    {        
        ISSerialMessage Create(string message, ushort preExecutionDelayMs = 0);        
    }
}
