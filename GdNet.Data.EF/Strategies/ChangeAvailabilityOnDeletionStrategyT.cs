using GdNet.Domain.Entity;

namespace GdNet.Data.EF.Strategies
{
    /// <summary>
    /// Just change the IsAvailable of entity to False
    /// </summary>
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