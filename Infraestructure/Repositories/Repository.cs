using Common;
using Common.Extensions;
using Effort.DataLoaders;
using Infraestructure.Context;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class Repository<T, K> : IRepository<T, K> where T : class, new() where K : DbContext, new()
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;
        private readonly IConfigurationLib config;

        public Common.Logging.Transaction Transaction { get; set; }


        public Repository(IAmbientDbContextLocator context, IConfigurationLib _config)
        {
            _ambientDbContextLocator = context;
            config = _config;
        }

        internal DbSet<T> dbSet
        {
            get
            {
                DbContext dbContext = null;
                dbContext = _ambientDbContextLocator.Get<K>();
                if (dbContext == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type UserManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");
                }
                else
                {
                    return dbContext.Set<T>();
                }
            }
        }

        public DbContext DbContext
        {
            get
            {
                DbContext dbContext = null;
                dbContext = _ambientDbContextLocator.Get<K>();
                if (dbContext == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type UserManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");
                }
                return dbContext;
            }
        }

        #region WithoutEResponse     
        public IQueryable<T> FindWithoutEResponse(Expression<Func<T, bool>> filter = null,
                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                 params Expression<Func<T, object>>[] includeProperties)
        {
            List<T> list = new List<T>();
            IQueryable<T> query = AsQueryable();
            IQueryable<T> queryWithInclusions = PerformInclusions(includeProperties, query);
            if (filter != null) queryWithInclusions = queryWithInclusions.Where(filter);
            if (orderBy != null) queryWithInclusions = orderBy(queryWithInclusions);
            queryWithInclusions = queryWithInclusions.Distinct();
            return queryWithInclusions;
        }

        public T SingleWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            T obj = new T();
            IQueryable<T> query = AsQueryable();
            IQueryable<T> queryWithInclusions = PerformInclusions(includeProperties, query);
            if (filter != null) obj = queryWithInclusions.Single(filter);
            else obj = queryWithInclusions.Single();
            //EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(obj);
            return obj;
        }

        public T SingleOrDefaultWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            T obj = new T();
            IQueryable<T> query = AsQueryable();
            IQueryable<T> queryWithInclusions = PerformInclusions(includeProperties, query);
            if (filter != null) obj = queryWithInclusions.SingleOrDefault(filter);
            else obj = queryWithInclusions.SingleOrDefault();
            //EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(obj);
            return obj;
        }

        public T FirstWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            T obj = new T();
            IQueryable<T> query = AsQueryable();
            IQueryable<T> queryWithInclusions = PerformInclusions(includeProperties, query);
            if (filter != null) obj = queryWithInclusions.First(filter);
            else obj = queryWithInclusions.First();
            //EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(obj);
            return obj;
        }

        public T FirstOrDefaultWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            T obj = new T();
            IQueryable<T> query = AsQueryable();
            IQueryable<T> queryWithInclusions = PerformInclusions(includeProperties, query);
            if (filter != null) obj = queryWithInclusions.FirstOrDefault(filter);
            else obj = queryWithInclusions.FirstOrDefault();
            //EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(obj);
            return obj;
        }

        public T LastWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            T obj = new T();
            IQueryable<T> query = AsQueryable();
            IQueryable<T> queryWithInclusions = PerformInclusions(includeProperties, query);
            if (filter != null) obj = queryWithInclusions.First(filter);
            else obj = queryWithInclusions.Last();
            return obj;
        }

        public T LastOrDefaultWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            T obj = new T();
            IQueryable<T> query = AsQueryable();
            IQueryable<T> queryWithInclusions = PerformInclusions(includeProperties, query);
            if (filter != null) obj = queryWithInclusions.FirstOrDefault(filter);
            else obj = queryWithInclusions.LastOrDefault();
            //EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(obj);
            return obj;
        }

        #endregion

        #region WithEResponse
        public EResponseBase<T> Find(Expression<Func<T, bool>> filter = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                            params Expression<Func<T, object>>[] includeProperties)
        {
            EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForList(FindWithoutEResponse(filter, orderBy, includeProperties));
            return response;
        }

        public EResponseBase<T> Single(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(SingleWithoutEResponse(filter, includeProperties));
            return response;
        }

        public EResponseBase<T> SingleOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(SingleOrDefaultWithoutEResponse(filter, includeProperties));
            return response;
        }

        public EResponseBase<T> First(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(FirstWithoutEResponse(filter, includeProperties));
            return response;
        }

        public EResponseBase<T> FirstOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(FirstOrDefaultWithoutEResponse(filter, includeProperties));
            return response;
        }

        public EResponseBase<T> Last(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(LastWithoutEResponse(filter, includeProperties));
            return response;
        }

        public EResponseBase<T> LastOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            EResponseBase<T> response = new UtilitariesResponse<T>(config).setResponseBaseForObj(LastOrDefaultWithoutEResponse(filter, includeProperties));
            return response;
        }

        public EResponseBase<T> Delete(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
            return new UtilitariesResponse<T>(config).setResponseBaseForOK(entities);
        }

        public EResponseBase<T> Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            if (entityToDelete == null)
            {
                return new UtilitariesResponse<T>(config).setResponseBaseForNoDataFound();
            }
            else
            {
                return Delete(entityToDelete);
            }
        }

        public EResponseBase<T> Delete(T entity)
        {
            if (entity == null) return new UtilitariesResponse<T>(config).setResponseBaseForNoDataFound();
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return new UtilitariesResponse<T>(config).setResponseBaseForOK(entity);
        }

        public EResponseBase<T> ChangeEnabled(object id, bool enabled)
        {
            T entityToChange = dbSet.Find(id);
            if (entityToChange is IAuditEntity)
            {
                ((IAuditEntity)entityToChange).Enabled = enabled;
                DbContext.Set<T>().Attach(entityToChange);
                DbContext.Entry(entityToChange).State = EntityState.Modified;
                return new UtilitariesResponse<T>(config).setResponseBaseForOK(entityToChange);
            }
            else
            {
                if (entityToChange == null)
                {
                    return new UtilitariesResponse<T>(config).setResponseBaseForNoDataFound();
                }
                else
                {
                    return new UtilitariesResponse<T>(config).setResponseBaseForException(new Exception("No se puede deshabilitar, debido a que la entidad no implementa la interfaz de Enabled"));
                }

            }
        }

        public EResponseBase<T> InsertOrUpdate(T entity, int id)
        {
            EResponseBase<T> response = new EResponseBase<T>();
            if (id != 0)
            {
                DbContext.Entry(entity).State = EntityState.Detached;
                response = Update(entity);
            }
            else
            {
                response = Insert(entity);
            }
            return response;
        }
        public EResponseBase<T> InsertOrUpdateReject(T entity, int id)
        {
            EResponseBase<T> response = new EResponseBase<T>();
            if (id != 0)
            {
                DbContext.Entry(entity).State = EntityState.Detached;
                response = Update(entity);
            }
            else
            {
                response = Insert(entity);
            }
            return response;
        }

        public EResponseBase<T> Insert(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return new UtilitariesResponse<T>(config).setResponseBaseForOK(entity);
        }

        public EResponseBase<T> Insert(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
            return new UtilitariesResponse<T>(config).setResponseBaseForOK(entities);
        }

        public EResponseBase<T> Update(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            return new UtilitariesResponse<T>(config).setResponseBaseForOK(entity);
        }

        public EResponseBase<T> Update(IEnumerable<T> entities)
        {
            foreach (T e in entities)
            {
                DbContext.Entry(e).State = EntityState.Modified;
            }
            return new UtilitariesResponse<T>(config).setResponseBaseForOK(entities);
        }
        #endregion

        #region SQL Queries
        public virtual EResponseBase<T> SelectQuery(string query, params object[] parameters)
        {
            IQueryable<T> queryReturn = DbContext.Set<T>().SqlQuery(query, parameters).AsQueryable();
            return new UtilitariesResponse<T>(config).setResponseBaseForList(queryReturn);
        }

        public virtual EResponseBase<T> ExecuteSqlCommand(string query, params object[] parameters)
        {
            int queryReturn = DbContext.Database.ExecuteSqlCommand(query, parameters);
            return new UtilitariesResponse<T>(config).setResponseBaseForExecuteSQLCommand(queryReturn);
        }

        //public EResponseBase<T> ExecuteSqlCommand(string query, params object[] parameters)
        //{
        //    var queryReturn = DbContext.Database.SqlQuery<T>(query, parameters).AsQueryable();
        //    return setResponseBaseForList(queryReturn);
        //}
        #endregion

        #region Utilities
        private IQueryable<T> AsQueryable()
        {
            return dbSet.AsQueryable();
        }
        private IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties,
                                                IQueryable<T> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }


        #endregion
    }
}
