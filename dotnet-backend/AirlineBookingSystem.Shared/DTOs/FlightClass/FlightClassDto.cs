namespace AirlineBookingSystem.Shared.DTOs.FlightClass;

/// <summary>
/// Represents a flight class data transfer object.
/// </summary>
public class FlightClassDto
{
    /// <summary>
    /// Gets or sets the ID of the flight class.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the ID of the flight.
    /// </summary>
    public required int FlightId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the class type.
    /// </summary>
    public int ClassId { get; set; }
    /// <summary>
    /// Gets or sets the price of the flight class.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the number of seats in the flight class.
    /// </summary>
    public int Seats { get; set; }
}
