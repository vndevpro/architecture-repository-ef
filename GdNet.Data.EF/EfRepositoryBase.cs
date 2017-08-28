using GdNet.Data.EF.Strategies;
using GdNet.Domain.Entity;
using GdNet.Domain.Repository;
using System;
using System.Data.Entity;

namespace GdNet.Data.EF
{
    public abstract class EfRepositoryBase<T> : EfRepositoryBaseT<T, Guid>, IRepositoryBase<T>
        where T : class, IAggregateRoot
    {
        protected EfRepositoryBase(IDbSet<T> entities)
            : base(entities)
        {
        }

        protected EfRepositoryBase(IDbSet<T> entities,
            ISavingStrategy savingStrategy,
            IDeletionStrategy<T> deletionStrategy,
            IFilterStrategy<T, Guid> filterStrategy)
            : base(entities, savingStrategy, deletionStrategy, filterStrategy)
        {
        }
    }
}
