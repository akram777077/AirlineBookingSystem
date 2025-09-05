namespace AirlineBookingSystem.Shared.Filters;

/// <summary>
/// Represents a filter for searching terminals.
/// </summary>
public class TerminalSearchFilter : PaginationFilter
{
    /// <summary>
    /// Gets or sets the ID of the airport where the terminal is located.
    /// </summary>
    public int? AirportId { get; set; }
    /// <summary>
    /// Gets or sets the name of the terminal to search for.
    /// </summary>
    public string? Name { get; set; }
}
