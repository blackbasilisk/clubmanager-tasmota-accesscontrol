using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.Library.Base.Infrastructure.Exceptions
{
    public class ThreadLockTimeoutException : System.Exception
    {
        public ThreadLockTimeoutException()
            :base("Thread lock attempt timed out. See StackTrace for details")
        {

        }
    }
}
