using SIS.Library.ModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Model
{
    public class ApplicationConfiguration : Entity
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
