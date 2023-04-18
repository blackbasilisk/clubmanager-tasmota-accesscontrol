using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Config
{
    public class ApplicationConfigurationCache
    {
     
        private string _connectionString;

        public string ConnectionString
        {
            get 
            {                
                return _connectionString;
            }
            set { _connectionString = value; }
        }

    }
}
