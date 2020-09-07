using Common;
using Effort.DataLoaders;
using Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public interface IRepository<T, K> where T : class, new() where K : class, new()
    {
        Common.Logging.Transaction Transaction { get; set; }
        DbContext DbContext { get; }

        #region IRepository<T> Members
        IQueryable<T> FindWithoutEResponse(Expression<Func<T, bool>> filter = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                params Expression<Func<T, object>>[] includeProperties);
        T SingleWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        T SingleOrDefaultWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        T FirstWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        T FirstOrDefaultWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        T LastWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        T LastOrDefaultWithoutEResponse(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        EResponseBase<T> Find(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includeProperties);

        EResponseBase<T> Single(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Retorna una entidad bajo una condición especificada o null sino encontrara registros
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        EResponseBase<T> SingleOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Retorna la primera entidad encontrada bajo una condición especificada
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        EResponseBase<T> First(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Retorna la primera entidad encontrada bajo una condición especificada o null sino encontrara registros
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        EResponseBase<T> FirstOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        EResponseBase<T> Last(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        EResponseBase<T> LastOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);

        EResponseBase<T> Delete(IEnumerable<T> entities);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="entity"></param>
        EResponseBase<T> Delete(object id);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="entity"></param>
        EResponseBase<T> Delete(T entity);

        /// <summary>
        /// Deshabilita/Habilita una entidad que implemente la interfaz AuditEntity
        /// </summary>
        /// <param name="entity"></param>
        EResponseBase<T> ChangeEnabled(object id, bool enabled);

        /// <summary>
        /// Registra o Actualiza una entidad
        /// </summary>
        /// <param name="entity"></param>
        EResponseBase<T> InsertOrUpdate(T entity, int id);
        EResponseBase<T> InsertOrUpdateReject(T entity, int id);

        /// <summary>
        /// Registra una entidad
        /// </summary>
        /// <param name="entity"></param>
        EResponseBase<T> Insert(T entity);

        EResponseBase<T> Insert(IEnumerable<T> entities);

        EResponseBase<T> Update(T entity);

        /// <summary>
        /// Actualiza varias entidades
        /// </summary>
        /// <param name="entity"></param>
        EResponseBase<T> Update(IEnumerable<T> entities);
        #endregion

        #region SQL Queries        
        EResponseBase<T> SelectQuery(string query, params object[] parameters);
        EResponseBase<T> ExecuteSqlCommand(string query, params object[] parameters);
        void SaveChanges();
        //IQueryable<I> ExecuteSqlCommand<I>(string query, params object[] parameters) where I : class;
        #endregion
    }
}
