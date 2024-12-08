using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SM.ClubManager.Library.Base.Interfaces;

namespace SM.ClubManager.Library.Base.BaseClasses
{
    public abstract class Entity : IEntity
    {
        public Int64 Id { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateModified { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
