namespace AirlineBookingSystem.Shared.DTOs.Seats;

/// <summary>
/// Represents a data transfer object for filtering available seats.
/// </summary>
public class GetAvailableSeatsFilterDto
{
    /// <summary>
    /// Gets or sets the ID of the flight.
    /// </summary>
    public int FlightId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the class type.
    /// </summary>
    public int? ClassTypeId { get; set; }
}