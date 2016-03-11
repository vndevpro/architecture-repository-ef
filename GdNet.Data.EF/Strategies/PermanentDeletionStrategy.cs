using GdNet.Domain.Entity;
using System.Data.Entity;

namespace GdNet.Data.EF.Strategies
{
    public class PermanentDeletionStrategy<T> : IDeletionStrategy<T> where T : class, IEditableEntity
    {
        private readonly IDbSet<T> _entities;

        public PermanentDeletionStrategy(IDbSet<T> entities)
        {
            _entities = entities;
        }

        public void Execute(T entity)
        {
            _entities.Remove(entity);
        }
    }
}