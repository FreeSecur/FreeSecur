﻿using FreeSecur.API.Core;
using FreeSecur.API.Core.GeneralHelpers;
using FreeSecur.API.Domain.Entities.Owners;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FreeSecur.API.Domain
{
    public class FsEntityRepository : IFsEntityRepository
    {
        private readonly FsDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        public FsEntityRepository(
            FsDbContext dbContext,
            IDateTimeProvider dateTimeProvider)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
        }

        
        public async Task<TEntity> GetEntity<TEntity>(
            Expression<Func<TEntity, bool>> whereClause)
            where TEntity : class
        {
            var dbSet = _dbContext.Set<TEntity>();

            var result = await dbSet
                .AsQueryable()
                .Where(whereClause)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return result;
        }

        
        public async Task<List<TEntity>> GetEntities<TEntity>(
            Expression<Func<TEntity, bool>> whereClause = null)
            where TEntity : class
        {
            var dbSet = _dbContext.Set<TEntity>();

            var result = await dbSet
                .AsQueryable()
                .Where(whereClause)
                .AsNoTracking()
                .ToListAsync();

            return result;
        }

        public async Task<TEntity> UpdateEntity<TEntity>(
            TEntity entity,
            long? userId)
            where TEntity : class, IFsEntity
        {

            if (entity is IFsTrackedEntity trackedEntity)
            {
                trackedEntity.ModifiedById = userId.Value;
                trackedEntity.ModifiedOn = _dateTimeProvider.Now;
            }

            var entry = _dbContext.Update(entity);

            if (entry.State == EntityState.Added)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
                throw new NotSupportedException("Not allowed to add entities through an update. Make sure the primary key is configured correctly");
            }

            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;


            //Save succesful return detached entity
            return entity;
        }

        
        public async Task<TEntity> AddEntity<TEntity>(
            TEntity entity,
            long? userId)
            where TEntity : class, IFsEntity
        {
            AddEntityModificationTracking(entity, userId);

            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;

            //Save succesful return detached entity
            return entity;
        }

        public async Task<List<TEntity>> AddEntities<TEntity>(List<TEntity> entities, long? userId)
            where TEntity : class, IFsEntity
        {
            foreach(var entity in entities)
            {
                AddEntityModificationTracking(entity, userId);
            }

            _dbContext.AddRange(entities);

            await _dbContext.SaveChangesAsync();

            foreach(var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }

            return entities;
        }

        public async Task RemoveEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> RemoveOwner<TEntity>(Expression<Func<TEntity, bool>> whereClause)
            where TEntity : class, IFsEntity, IOwner
        {
            var dbSet = _dbContext.Set<TEntity>();

            var entityToRemove = await dbSet.Where(whereClause).Include(x => x.Owner).SingleOrDefaultAsync();

            if (entityToRemove == null)
            {
                return null;
            }

            _dbContext.Remove(entityToRemove);
            _dbContext.Remove(entityToRemove.Owner);

            await _dbContext.SaveChangesAsync();

            return entityToRemove;
        }

        public async Task<TEntity> AddOwner<TEntity>(TEntity entity, long? userId)
            where TEntity: class, IFsEntity, IOwner
        {
            var owner = new Owner
            {

            };

            entity.Owner = owner;

            AddEntityModificationTracking(entity, userId);

            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }


        private void AddEntityModificationTracking<TEntity>(TEntity entity, long? userId) where TEntity : class, IFsEntity
        {
            if (entity is IFsTrackedEntity trackedEntity)
            {
                trackedEntity.CreatedById = userId.Value;
                trackedEntity.CreatedOn = _dateTimeProvider.Now;
                trackedEntity.ModifiedById = userId.Value;
                trackedEntity.ModifiedOn = _dateTimeProvider.Now;
            }
        }
    }
}
