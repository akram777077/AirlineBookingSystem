using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Shared.Results;

/// <summary>
/// Extension methods for creating paged results.
/// </summary>
public static class PagedResultExtensions
{
    /// <summary>
    /// Creates a paged result from an <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <param name="query">The query.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A paged result.</returns>
    public static async Task<PagedResult<List<T>>> ToPagedResult<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        var totalCount = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<List<T>>(items, pageNumber, pageSize, totalCount);
    }
}
