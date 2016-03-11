using GdNet.Domain.Entity;

namespace GdNet.Data.EF.Strategies
{
    public interface IDeletionStrategy<in T> where T : IEditableEntity
    {
        void Execute(T entity);
    }
}