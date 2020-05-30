using System.Linq;


namespace Ottobo.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int page, int recordsPerPage)
        {
            return queryable
                .Skip((page - 1) * recordsPerPage)
                .Take(recordsPerPage);
        }
    }
}