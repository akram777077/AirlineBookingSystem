namespace AirlineBookingSystem.Shared.DTOs.Gates;

/// <summary>
/// Represents a data transfer object for creating a gate.
/// </summary>
public struct CreateGateDto
{
    /// <summary>
    /// Gets or sets the gate number.
    /// </summary>
    public required string GateNumber { get; set; }
    /// <summary>
    /// Gets or sets the ID of the terminal where the gate is located.
    /// </summary>
    public int TerminalId { get; set; }
}
