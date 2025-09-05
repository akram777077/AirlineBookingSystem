namespace AirlineBookingSystem.Shared.DTOs.terminals;

/// <summary>
/// Represents a data transfer object for updating a terminal.
/// </summary>
public struct UpdateTerminalDto
{
    /// <summary>
    /// Gets or sets the ID of the terminal.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the terminal.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Gets or sets the ID of the airport where the terminal is located.
    /// </summary>
    public int AirportId { get; set; } 
    
}
