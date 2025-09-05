namespace AirlineBookingSystem.Shared.DTOs.terminals;

/// <summary>
/// Represents a data transfer object for creating a terminal.
/// </summary>
public struct CreateTerminalDto
{
    /// <summary>
    /// Gets or sets the name of the terminal.
    /// </summary>
    public required string Name { get; set; } 
    /// <summary>
    /// Gets or sets the ID of the airport where the terminal is located.
    /// </summary>
    public int AirportId { get; set; } 
}
