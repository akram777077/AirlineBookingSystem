namespace AirlineBookingSystem.Shared.Filters;

/// <summary>
/// Represents a filter for searching airplanes.
/// </summary>
public class AirplaneSearchFilter : PaginationFilter
{
    /// <summary>
    /// Gets or sets the model of the airplane to search for.
    /// </summary>
    public string? Model { get; set; }
    /// <summary>
    /// Gets or sets the manufacturer of the airplane to search for.
    /// </summary>
    public string? Manufacturer { get; set; }
}
