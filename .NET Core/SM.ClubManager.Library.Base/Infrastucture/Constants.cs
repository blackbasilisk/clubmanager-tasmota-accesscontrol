using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.Library.Base.Infrastructure
{
    public static class Constants
    {
        public const string EventLogName = "Creative IT";
        public const string EFConnectionStringTemplate = "metadata=res://*/Model.{0}.csdl|res://*/Model.{0}.ssdl|res://*/Model.{0}.msl;provider=System.Data.SqlClient;provider connection string='{1};App={2}'";
        public const string DropDownNullValueDisplay = "<Not selected>";

        public static class SpecialChars
        {
            public const string BulletPoint = "\u2022";
        }
    }
}
