
using System.Reflection;
using System.Collections.Generic;

namespace AirlineBookingSystem.Shared.Filters;

public record PaginationFilter(int PageNumber = 1, int PageSize = 10)
{
    private const int MaxPageSize = 20;

    public int PageSize { get; init; } = PageSize > MaxPageSize ? MaxPageSize : PageSize;

    public IDictionary<string, string> ToDictionary()
    {
        var dictionary = new Dictionary<string, string>();
        foreach (var prop in GetType().GetProperties())
        {
            var value = prop.GetValue(this);
            if (value != null)
            {
                dictionary.Add(prop.Name, value.ToString()!);
            }
        }
        return dictionary;
    }
}
