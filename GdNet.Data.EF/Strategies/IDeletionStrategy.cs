using System;
using GdNet.Domain.Entity;

namespace GdNet.Data.EF.Strategies
{
    public interface IDeletionStrategy<in T> : IDeletionStrategyT<T, Guid>
        where T : IEditableEntity
    {
    }
}