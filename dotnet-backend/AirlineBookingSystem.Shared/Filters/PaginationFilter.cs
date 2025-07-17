
using System.Reflection;
using System.Collections.Generic;

namespace AirlineBookingSystem.Shared.Filters;

public class PaginationFilter
{
    private const int MaxPageSize = 20;
    private int _pageSize = 10;
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value;
    }

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
