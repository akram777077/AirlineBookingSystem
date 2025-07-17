using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Shared.Results;

public class PagedResult<T> : Result<T>
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalPages { get; private set; }
    public int TotalRecords { get; private set; }

    public PagedResult(T data, int pageNumber, int pageSize, int totalRecords) : base(data, null, ResultStatusCode.Success, null)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }

    public static async Task<PagedResult<List<T>>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<List<T>>(items, pageNumber, pageSize, count);
    }
}
