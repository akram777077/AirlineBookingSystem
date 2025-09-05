using AirlineBookingSystem.Shared.Filters;

namespace AirlineBookingSystem.Shared.DTOs.Gates;

/// <summary>
/// Represents a filter for searching gates.
/// </summary>
public class GateSearchFilter : PaginationFilter
{
    /// <summary>
    /// Gets or sets the gate number to search for.
    /// </summary>
    public string? GateNumber { get; set; }
    /// <summary>
    /// Gets or sets the ID of the terminal where the gate is located.
    /// </summary>
    public int? TerminalId { get; set; }
}
