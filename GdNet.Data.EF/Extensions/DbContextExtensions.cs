using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace GdNet.Data.EF.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// A shorthand to use CreateQuery instead of ((IObjectContextAdapter)context).ObjectContext.CreateQuery
        /// </summary>
        public static ObjectQuery<T> CreateQuery<T>(this DbContext context, string query, params ObjectParameter[] parameters)
        {
            return ((IObjectContextAdapter)context).ObjectContext.CreateQuery<T>(query, parameters);
        }

        /// <summary>
        /// A shorthand to use CreateObjectSet instead of ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet
        /// </summary>
        public static ObjectSet<T> CreateObjectSet<T>(this DbContext context) where T : class
        {
            return ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<T>();
        }

        /// <summary>
        /// Get IObjectContextAdapter from the context
        /// </summary>
        public static ObjectContext GetObjectContext(this DbContext context)
        {
            return ((IObjectContextAdapter)context).ObjectContext;
        }
    }
}
