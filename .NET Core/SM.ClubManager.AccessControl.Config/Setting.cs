using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Config
{
    public class Setting
    {
        public Type Type { get; set; }

        public object Value { get; set; }

        public string Name { get; set; }
    }
}
