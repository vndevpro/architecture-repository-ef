using System;
using GdNet.Domain.Entity;

namespace GdNet.Data.EF.Strategies
{
    public class ChangeAvailabilityOnDeletionStrategy<T> : ChangeAvailabilityOnDeletionStrategyT<T, Guid>, IDeletionStrategy<T>
        where T : class, IEditableEntity
    {
    }
}