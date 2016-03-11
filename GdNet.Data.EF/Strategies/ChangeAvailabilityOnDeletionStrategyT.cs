using GdNet.Domain.Entity;

namespace GdNet.Data.EF.Strategies
{
    public class ChangeAvailabilityOnDeletionStrategyT<T, TId> : IDeletionStrategyT<T, TId>
        where T : class, IEditableEntityT<TId>
        where TId : new()
    {
        public void Execute(T entity)
        {
            entity.IsAvailable = false;
        }
    }
}