using GdNet.Domain.Entity;

namespace GdNet.Data.EF.Strategies
{
    public interface IDeletionStrategyT<in T, TId>
        where T : IEditableEntityT<TId>
        where TId : new()
    {
        void Execute(T entity);
    }
}