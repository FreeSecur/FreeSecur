using FreeSecur.API.Domain.Entities.Owners;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreeSecur.API.Domain
{
    /// <summary>
    /// Repository with standard database actions for entities
    /// </summary>
    public interface IFsEntityRepository
    {
        /// <summary>
        /// Gets single entity based on criteria
        /// Throws exception if the multiple matches are made
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereClause"></param>
        /// <returns>Untracked entity</returns>
        Task<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> whereClause) where TEntity : class;
        /// <summary>
        /// Gets list of entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereClause"></param>
        /// <returns>List of untracked entities</returns>
        Task<List<TEntity>> GetEntities<TEntity>(Expression<Func<TEntity, bool>> whereClause = null) where TEntity : class;
        /// <summary>
        /// Attaches entity and updates entity in the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <returns>Untracked updated entity</returns>
        Task<TEntity> UpdateEntity<TEntity>(TEntity entity, int? userId) where TEntity : class, IFsEntity;
        /// <summary>
        /// Attaches entity and adds entity to the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <returns>Untracked updated entity</returns>
        Task<TEntity> AddEntity<TEntity>(TEntity entity, int? userId) where TEntity : class, IFsEntity;
        /// <summary>
        /// Attaches and adds range of entities to the databases
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<TEntity>> AddEntities<TEntity>(List<TEntity> entities, int? userId) where TEntity : class, IFsEntity;
        /// <summary>
        /// Removes detached entity from database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveEntity<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// Creates entity and adds a new vault owner to the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <returns>Untracked created entity</returns>
        Task<TEntity> AddOwner<TEntity>(TEntity entity, int? userId) where TEntity : class, IFsEntity, IOwner;
        /// <summary>
        /// Removes entity and removes owner from the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        Task<TEntity> RemoveOwner<TEntity>(Expression<Func<TEntity, bool>> whereClause) where TEntity : class, IFsEntity, IOwner;
    }
}