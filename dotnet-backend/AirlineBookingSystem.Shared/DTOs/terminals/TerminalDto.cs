namespace AirlineBookingSystem.Shared.DTOs.terminals;

/// <summary>
/// Represents a terminal data transfer object.
/// </summary>
public struct TerminalDto
{
    /// <summary>
    /// Gets or sets the ID of the terminal.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the terminal.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the ID of the airport where the terminal is located.
    /// </summary>
    public int AirportId { get; set; }
}
