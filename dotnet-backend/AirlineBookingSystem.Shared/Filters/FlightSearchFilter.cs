namespace AirlineBookingSystem.Shared.Filters;

/// <summary>
/// Represents a filter for searching flights.
/// </summary>
public class FlightSearchFilter: PaginationFilter
{
    /// <summary>
    /// Gets or sets the ID of the departure city.
    /// </summary>
    public int? FromCityId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the arrival city.
    /// </summary>
    public int? ToCityId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the departure country.
    /// </summary>
    public int? FromCountryId { get; set; } 
    /// <summary>
    /// Gets or sets the ID of the arrival country.
    /// </summary>
    public int? ToCountryId { get; set; } 
    /// <summary>
    /// Gets or sets the departure date of the flight.
    /// </summary>
    public DateTimeOffset? DepartureDate { get; set; }
}
