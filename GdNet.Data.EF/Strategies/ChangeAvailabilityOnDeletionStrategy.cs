using GdNet.Domain.Entity;

namespace GdNet.Data.EF.Strategies
{
    public class ChangeAvailabilityOnDeletionStrategy<T> : IDeletionStrategy<T> where T : class, IEditableEntity
    {
        public void Execute(T entity)
        {
            entity.IsAvailable = false;
        }
    }
}