using SM.ClubManager.Library.Base.Infrastructure;
using SM.ClubManager.Library.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;

namespace SM.ClubManager.Library.Base.BaseClasses
{
    public class RepositoryBase<T> : IDisposable, IRepository<T> where T : Entity , new()
    {
        protected DbSet<T> DbSet;
        protected internal DbContext context;
        public string _connectionString;
        private bool isDisposeContext = false;

        public string ConnectionString { get { return _connectionString; } }

        public RepositoryBase(DbContext dataContext, bool autoDisposeContext = false)
        {
            if (dataContext == null)
            {              
                throw new NullReferenceException("DataContext cannot be null. Please ensure a context is passed to the method.");
                //throw new NotSupportedException("The application does not support a repository that takes no parameters");
            }
            this.DbSet = dataContext.Set<T>();
            this.context = dataContext;
        }

        #region IRepository<T> Members

        //public void Insert(T entity)
        //{a
        //    DbSet.Add(entity);
        //}

        public void InsertOrUpdate(T entity, bool isSaveChanges = false)
        {

            //if the entity is detached, try and find it. 
            //if it cant be found, attach it, 
            //else check the currentvalues["Id"] if it's 0 to see if we are supposed to add / update            
            if (context.Entry<T>(entity).State == EntityState.Detached || Convert.ToInt64(context.Entry<T>(entity).CurrentValues["Id"]) == default(long))
            {
                context.Entry(entity).Property<DateTime>("DateModified").CurrentValue = DateTime.Now;
                context.Entry(entity).Property<DateTime>("DateCreated").CurrentValue = DateTime.Now;
                context.Entry(entity).State = EntityState.Added;
                DbSet.Add(entity);
            }
            else
            {
                context.Entry(entity).Property<DateTime>("DateModified").CurrentValue = DateTime.Now;
                context.Entry(entity).State = EntityState.Modified;               
            }
            if (isSaveChanges)
            {
                context.SaveChanges();
            }
                //SaveChanges();
        }

        public void InsertBulk(IEnumerable<T> entities, bool isSaveChanges = false)
        {
            int counter = 0;
            context.Set(typeof(T)).AddRange(entities);
            //foreach (var item in entities)
            //{
            //    //context.Entry(item).Property<DateTime>("DateModified").CurrentValue = DateTime.Now;
            //    //context.Entry(item).Property<DateTime>("DateCreated").CurrentValue = DateTime.Now;
            //    context.Entry(item).State = EntityState.Added;
            //    counter++;
            //}
            //DbSet.AddRange(entities);
                        
            if (isSaveChanges)
            {
                context.SaveChanges();
            }
            //SaveChanges();
        }

        internal DbContext Database { get { return context; } }

        //public void Delete(T entity)
        //{
        //    DbSet.Remove(entity);
        //}

        public void Delete(T entity, bool isPermanent = false)
        {
            if (!isPermanent)
                //entity.IsDeleted = true;
                context.Entry(entity).Property<bool>("IsDeleted").CurrentValue = true;
            else
                DbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes all the entries permanently! It does not just flag it as deleted
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteBulk(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(e => e.IsDeleted == false).Where(predicate);
            //return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll(bool includeDeleted = false)
        {
            
            if (includeDeleted)         
                return DbSet;                                    
            else
                return DbSet.Where(e => e.IsDeleted == false);           
        }

        public DbSet<T> GetAllFaster()
        {
            return DbSet;
        }

        public DbSet<T> GetDbSet(bool includeDeleted = false)
        {           
                return DbSet;           
        }

        public DbSet<T> GetDBSet(bool includeDeleted = false)
        {
            return DbSet;
        }
     
        public T GetById(int id)
        {
            var entity = DbSet.Find(id);
            return context.Entry(entity).Entity;
        }

        public T GetById(long id)
        {
            try
            {
                this.context.ChangeTracker.DetectChanges();
                var entity = DbSet.Find(id);
                if (entity.IsDeleted)
                    return null;
                return context.Entry(entity).Entity;
            }
            catch (IndexOutOfRangeException indexEd)
            {
                this.context.ChangeTracker.DetectChanges();
                try
                {
                    var entity = DbSet.Find(id);
                    if (entity.IsDeleted)
                        return null;
                    return context.Entry(entity).Entity;
                }
                catch (IndexOutOfRangeException ex)
                {
                    
                    throw ex;
                }

                throw indexEd;
            }
            
        }

        public int SaveChanges()
        {
            int result = -1;
            try
            {                
                result = context.SaveChanges();
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex); // Add the original exception as the innerException
            }
            catch(System.Data.Entity.Infrastructure.CommitFailedException commitEx)
            {
                EventlogHelper eventLog = new EventlogHelper();
                eventLog.AddEntry(commitEx.Message, 99, System.Diagnostics.EventLogEntryType.Error, commitEx);
            }
            catch (Exception ex)
            {
                EventlogHelper eventLog = new EventlogHelper();
                eventLog.AddEntry(ex.Message, 99, System.Diagnostics.EventLogEntryType.Error, ex);

                throw ex;                
            }
            return result;
        }

        public void Reload(T Entity) 
        {
            context.Entry<T>(Entity).Reload();
        }

        #endregion IRepository<T> Members

        public void Dispose()
        {
            //if (isDisposeContext)
            //{
            //    _unitOfWork._context.Dispose();
            //    //if (_unitOfWork.context != null)
            //    //    _unitOfWork.context = null;
            //}
            
 
            
        }
    }
}