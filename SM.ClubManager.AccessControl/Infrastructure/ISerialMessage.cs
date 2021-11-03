using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Infrastructure
{
    public interface ISerialMessage 
    {
        ISerialMessage Create(string messageString);
    }
}
