using GdNet.Domain.Entity;
using System;

namespace GdNet.Data.EF.Strategies
{
    public interface IFilterStrategy<T, TId>
        where T : IEditableEntityT<TId>
        where TId : new()
    {
        Func<T, bool> Predicate { get; }
    }
}