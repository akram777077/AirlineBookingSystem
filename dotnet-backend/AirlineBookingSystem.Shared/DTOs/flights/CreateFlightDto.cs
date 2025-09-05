namespace AirlineBookingSystem.Shared.DTOs.flights;

/// <summary>
/// Represents a data transfer object for creating a flight.
/// </summary>
public struct CreateFlightDto
{
    /// <summary>
    /// Gets or sets the departure time of the flight.
    /// </summary>
    public DateTimeOffset DepartureTime { get; set; }
    /// <summary>
    /// Gets or sets the arrival time of the flight.
    /// </summary>
    public DateTimeOffset? ArrivalTime { get; set; }
    /// <summary>
    /// Gets or sets the ID of the airplane for the flight.
    /// </summary>
    public int AirplaneId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the arrival gate for the flight.
    /// </summary>
    public int? ArrivalGateId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the departure gate for the flight.
    /// </summary>
    public int DepartureGateId { get; set; }
}
