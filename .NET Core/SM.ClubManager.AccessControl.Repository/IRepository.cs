using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.AccessControl.Repository
{
    public interface IRepository<T> : IDisposable
    {
        void InsertOrUpdate(T entity, bool IsSaveChanges = false);
        void Delete(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        Task<List<T>> GetAllListAsync();


        T GetByKey(string key);
   

        Task<T> GetByKeyAsync(string key);
    }
}
