using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.ClubManager.Library.Base.Interfaces
{
    interface IEntity
    {
        Int64 Id { get; set; }
       bool IsDeleted { get; set; }
       System.DateTime DateModified { get; set; }
       System.DateTime DateCreated { get; set; }
    }
}
