using GdNet.Common;
using GdNet.Data.EF.Strategies;
using GdNet.Domain.Entity;
using GdNet.Domain.Exceptions;
using GdNet.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GdNet.Data.EF
{
    public abstract class EfRepositoryBaseT<T, TId> : IRepositoryBaseT<T, TId>
        where T : class, IAggregateRootT<TId>
        where TId : new()
    {
        protected readonly IDbSet<T> Entities;
        protected readonly ISavingStrategy SavingStrategy;
        protected readonly IFilterStrategy<T, TId> FilterStrategy;

        private readonly IDeletionStrategyT<T, TId> _deletionStrategy;

        /// <summary>
        /// By default, this constructor uses EmptySavingStrategy and ChangeAvailabilityOnDeletionStrategy
        /// </summary>
        protected EfRepositoryBaseT(IDbSet<T> entities)
            : this(entities,
                new EmptySavingStrategy(),
                new ChangeAvailabilityOnDeletionStrategyT<T, TId>(),
                new EntityIsAvailableFilterStrategy<T, TId>())
        {
        }

        /// <summary>
        /// Set deletion & saving strategies explicitly
        /// </summary>
        protected EfRepositoryBaseT(IDbSet<T> entities,
            ISavingStrategy savingStrategy,
            IDeletionStrategyT<T, TId> deletionStrategy,
            IFilterStrategy<T, TId> filterStrategy)
        {
            Entities = entities;
            SavingStrategy = savingStrategy;
            _deletionStrategy = deletionStrategy;
            FilterStrategy = filterStrategy;
        }

        /// <summary>
        /// Count all entities in the system
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return Entities.Count(FilterStrategy.Predicate);
        }

        /// <summary>
        /// Count all entities in the system which match the filter
        /// </summary>
        public long Count(Func<T, bool> filter)
        {
            return Entities.Count(filter);
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public void Delete(T entity)
        {
            _deletionStrategy.Execute(entity);
        }

        public void Delete(TId id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public T GetById(TId id)
        {
            var entity = Entities.FirstOrDefault(e => (object)e.Id == (object)id);

            if (entity == null)
            {
                throw new EntityNotFoundException<TId>(id);
            }

            return entity;
        }

        /// <summary>
        /// Get page of entities with default filter from filterStrategy.Predicate
        /// </summary>
        public Result<T> Get(Page page)
        {
            return OnGet(Entities.OrderByDescending(x => x.LastModifiedAt), page);
        }

        /// <summary>
        /// Get page of entities with custom filter (not include the predicate from filterStrategy)
        /// </summary>
        public Result<T> Get(Page page, Func<T, bool> filter)
        {
            return OnGet(Entities.OrderByDescending(x => x.LastModifiedAt), page, filter);
        }
        
        /// <summary>
        /// Get an entity by given filter
        /// </summary>
        public T GetByFilter(Func<T, bool> filter)
        {
            return Entities.FirstOrDefault(filter);
        }

        /// <summary>
        /// Save a collection of entities
        /// </summary>
        public IEnumerable<T> Save(IEnumerable<T> entities)
        {
            return entities.Select(Save).ToList();
        }

        /// <summary>
        /// Save one entity
        /// </summary>
        public T Save(T entity)
        {
            SavingStrategy.OnSaving();

            Entities.AddOrUpdate(entity);

            SavingStrategy.OnSaved();

            return entity;
        }

        /// <summary>
        /// Get entities with applying default filter from filterStrategy.Predicate
        /// </summary>
        protected Result<T> OnGet(IEnumerable<T> entities, Page page)
        {
            return OnGet(entities, page, FilterStrategy.Predicate);
        }

        /// <summary>
        /// Get entities with applying custom filter (not include the predicate from filterStrategy)
        /// </summary>
        protected Result<T> OnGet(IEnumerable<T> entities, Page page, Func<T, bool> filter)
        {
            var offset = page.PageIndex * page.ItemsPerPage;
            var pagedEntities = entities.Where(filter).Skip(offset).Take(page.ItemsPerPage);

            return new Result<T>(pagedEntities)
            {
                Total = Count(filter)
            };
        }
    }
}