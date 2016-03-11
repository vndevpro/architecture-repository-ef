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
    public abstract class EfRepositoryBase<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
        protected readonly IDbSet<T> Entities;
        protected readonly ISavingStrategy SavingStrategy;
        private readonly IDeletionStrategy<T> _deletionStrategy;

        /// <summary>
        /// By default, this constructor uses EmptySavingStrategy and ChangeAvailabilityOnDeletionStrategy
        /// </summary>
        protected EfRepositoryBase(IDbSet<T> entities)
            : this(entities, new EmptySavingStrategy(), new ChangeAvailabilityOnDeletionStrategy<T>())
        {
        }

        protected EfRepositoryBase(IDbSet<T> entities, ISavingStrategy savingStrategy, IDeletionStrategy<T> deletionStrategy)
        {
            Entities = entities;
            SavingStrategy = savingStrategy;
            _deletionStrategy = deletionStrategy;
        }

        public long Count()
        {
            return Entities.Count();
        }

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

        public void Delete(Guid id)
        {
            var entity = GetById(id);
            Delete(entity);
        }

        public T GetById(Guid id)
        {
            var entity = Entities.FirstOrDefault(e => e.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException(id);
            }

            return entity;
        }

        public Result<T> Get(Page page)
        {
            return OnGet(Entities.OrderByDescending(x => x.LastModifiedAt), page);
        }

        public Result<T> Get(Page page, Func<T, bool> filter)
        {
            return OnGet(Entities.Where(filter).OrderByDescending(x => x.LastModifiedAt), page, filter);
        }

        public T GetByFilter(Func<T, bool> filter)
        {
            return Entities.FirstOrDefault(filter);
        }

        public IEnumerable<T> Save(IEnumerable<T> entities)
        {
            return entities.Select(Save);
        }

        public T Save(T entity)
        {
            SavingStrategy.OnSaving();

            Entities.AddOrUpdate(entity);

            SavingStrategy.OnSaved();

            return entity;
        }

        protected Result<TEntity> OnGet<TEntity>(IEnumerable<TEntity> entities, Page page)
        {
            var offset = page.PageIndex * page.ItemsPerPage;
            var pagedEntities = entities.Skip(offset).Take(page.ItemsPerPage);

            return new Result<TEntity>(pagedEntities)
            {
                Total = Count()
            };
        }

        protected Result<TEntity> OnGet<TEntity>(IEnumerable<TEntity> entities, Page page, Func<T, bool> filter)
        {
            var offset = page.PageIndex * page.ItemsPerPage;
            var pagedEntities = entities.Skip(offset).Take(page.ItemsPerPage);

            return new Result<TEntity>(pagedEntities)
            {
                Total = Count(filter)
            };
        }
    }
}
