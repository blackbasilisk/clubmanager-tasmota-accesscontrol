
using SIS.Library.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Model
{
    public class SMApplicationConfigurationItem : Entity
    {
        
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
