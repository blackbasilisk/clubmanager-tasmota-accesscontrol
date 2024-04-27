using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Model
{
    public class ConfigurationSaveException : Exception
    {
        public ConfigurationSaveException(string configKeyName, Exception ex)
            : base(string.Format("Error while saving configuration for {0}, see inner exception for details.", configKeyName), ex)
        {

        }
    }
}
