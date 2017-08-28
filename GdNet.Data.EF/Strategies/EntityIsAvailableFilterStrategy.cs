using GdNet.Domain.Entity;
using System;

namespace GdNet.Data.EF.Strategies
{
    public class EntityIsAvailableFilterStrategy<T, TId> : IFilterStrategy<T, TId>
        where T : class, IEditableEntityT<TId>
        where TId : new()
    {
        public Func<T, bool> Predicate
        {
            get { return x => x.IsAvailable; }
        }
    }
}