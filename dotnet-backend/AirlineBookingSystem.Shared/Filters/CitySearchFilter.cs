using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.DTOs.Cities;

/// <summary>
/// Represents a filter for searching cities.
/// </summary>
public class CitySearchFilter : PaginationFilter
{
    /// <summary>
    /// Gets or sets the ID of the country where the city is located.
    /// </summary>
    public int? CountryId { get; set; }
    /// <summary>
    /// Gets or sets the name of the city to search for.
    /// </summary>
    public string? Name { get; set; }
}
