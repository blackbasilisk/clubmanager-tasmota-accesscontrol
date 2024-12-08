using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SM.ClubManager.Library.Base.Infrastructure
{
    public class Configuration : IDisposable 
    {
        public Configuration() { }

        public string GetValue(string key)
        {
            string returnValue = "";

            var value = ConfigurationManager.AppSettings[key];
            if (value != null)
            {
                returnValue = ConfigurationManager.AppSettings[key].ToString();
            }
            return returnValue;
        }
            
        public void Dispose() 
        {
            
        }
    }    
}
