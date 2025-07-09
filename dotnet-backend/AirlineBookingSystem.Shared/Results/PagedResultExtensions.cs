using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Shared.Results;

public static class PagedResultExtensions
{
    public static async Task<PagedResult<List<T>>> ToPagedResult<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        var totalRecords = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<List<T>>(items, pageNumber, pageSize, totalRecords);
    }
}
