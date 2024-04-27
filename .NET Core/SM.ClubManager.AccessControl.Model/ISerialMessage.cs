using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Model
{
    public interface ISerialMessage : IDisposable
    {        
        ISerialMessage Create(string message, int preExecutionDelayMs = 0);
        
    }
}
