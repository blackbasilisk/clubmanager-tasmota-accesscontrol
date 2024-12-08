using SM.ClubManager.AccessControl.Model;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SM.ClubManager.ModelBase;

namespace SM.ClubManager.AccessControl.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        protected DbContext context;   

        public Repository(DbContext dataContext)
        {
            DbSet = dataContext.Set<T>();
            context = dataContext;
        }

        //public Repository(DbContext dataContext) : this(dataContext)
        //{
        //}

        #region IRepository<T> Members

        //public void Insert(T entity)
        //{
        //    DbSet.Add(entity);
        //}
        public void Reload(T Entity)
        {
            context.Entry<T>(Entity).Reload();
        }

        public void InsertOrUpdate(T entity, bool IsSaveChanges = false)
        {

            if (((IEntity)entity).Id == default)
            {
                DbSet.Add(entity);
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }

            if (IsSaveChanges)
                SaveChanges();

        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }
        public async Task<List<T>> GetAllListAsync()
        {
            return await DbSet.ToListAsync();
        }

        public T GetByKey(string key)
        {
            return DbSet.Find(key);
        }
        public async Task<T> GetByKeyAsync(string key)
        {
            return await DbSet.FindAsync(key);
        }
        #endregion

        public void Dispose()
        {

        }
    }
}
