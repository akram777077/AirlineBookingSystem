namespace AirlineBookingSystem.Shared.Results;

/// <summary>
/// Represents a paged result.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class PagedResult<T> : Result<T>
{
    /// <summary>
    /// Gets the page number.
    /// </summary>
    public int PageNumber { get; }
    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int PageSize { get; }
    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; }
    /// <summary>
    /// Gets the total count of items.
    /// </summary>
    public int TotalCount { get; }
    /// <summary>
    /// Gets the metadata.
    /// </summary>
    public Dictionary<string, string> Metadata { get; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="totalCount">The total count of items.</param>
    public PagedResult(T value, int pageNumber, int pageSize, int totalCount) : base(value, true, ResultStatusCode.Success, string.Empty)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
