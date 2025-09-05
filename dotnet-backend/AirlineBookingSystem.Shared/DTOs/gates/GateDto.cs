namespace AirlineBookingSystem.Shared.DTOs.Gates;

/// <summary>
/// Represents a gate data transfer object.
/// </summary>
public struct GateDto
{
    /// <summary>
    /// Gets or sets the ID of the gate.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the gate number.
    /// </summary>
    public required string GateNumber { get; set; }
    /// <summary>
    /// Gets or sets the ID of the terminal where the gate is located.
    /// </summary>
    public int TerminalId { get; set; }
    /// <summary>
    /// Gets or sets the name of the terminal where the gate is located.
    /// </summary>
    public required string TerminalName { get; set; }
}
