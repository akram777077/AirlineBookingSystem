using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.Filters;

/// <summary>
/// Represents a filter for searching airports.
/// </summary>
public class AirportSearchFilter : PaginationFilter
{
    /// <summary>
    /// Gets or sets the code of the airport to search for.
    /// </summary>
    public string? AirportCode { get; set; }
    /// <summary>
    /// Gets or sets the name of the airport to search for.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Gets or sets the ID of the city where the airport is located.
    /// </summary>
    public int? CityId { get; set; }
}