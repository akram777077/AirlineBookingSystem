namespace AirlineBookingSystem.Shared.DTOs.FlightStatuses;

/// <summary>
/// Represents a flight status data transfer object.
/// </summary>
public struct FlightStatusDto
{
    /// <summary>
    /// Gets or sets the ID of the flight status.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the flight status.
    /// </summary>
    public required string Name { get; set; }
}
