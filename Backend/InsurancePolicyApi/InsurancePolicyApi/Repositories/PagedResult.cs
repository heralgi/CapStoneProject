using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyApi.Repositories
{
    /// <summary>A materialized page of entities plus the total matching count (REP-RUL-006).</summary>
    public record PagedResult<T>(IReadOnlyList<T> Items, long TotalRecords);

    /// <summary>Pagination helper for composed queries. Kept in the data layer, free of DTO/HTTP types.</summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Counts the total matching rows, then returns the requested page.
        /// Caller is responsible for having applied filtering and sorting first.
        /// </summary>
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var total = await query.LongCountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<T>(items, total);
        }
    }
}
