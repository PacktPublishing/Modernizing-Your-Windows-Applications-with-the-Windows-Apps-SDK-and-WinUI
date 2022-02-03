using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Framework
{
    public static class QueryableExtensions
    {
        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> queryable)
        {
            // Do not take this as a good example. In EF, this is mapped to the actual SQL query.
            return Task.Factory.StartNew<List<T>>(queryable.ToList);
        }
    }
}