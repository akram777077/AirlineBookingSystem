
using System.Reflection;
using System.Collections.Generic;

namespace AirlineBookingSystem.Shared.Filters;

/// <summary>
/// Represents a pagination filter.
/// </summary>
public class PaginationFilter
{
    private const int MaxPageSize = 20;
    private int _pageSize = 10;
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value;
    }

    /// <summary>
    /// Converts the filter to a dictionary.
    /// </summary>
    /// <returns>A dictionary representation of the filter.</returns>
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
