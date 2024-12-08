using System;
using System.Collections.Generic;
//using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SM.ClubManager.Library.Base.Interfaces
{
    interface IRepository<T>
    {
        void InsertOrUpdate(T entity, bool isSaveChanges = false);
        void Delete(T entity, bool isPermanent = false);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(bool includeDeleted = false);
        T GetById(int id);
        T GetById(long id);
        string ConnectionString { get; }

        void Reload(T entity);
    }
}
