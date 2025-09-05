using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a flight class entity.
/// </summary>
public class FlightClass
{
    /// <summary>
    /// Gets or sets the ID of the flight class.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the ID of the flight.
    /// </summary>
    public int FlightId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the class type.
    /// </summary>
    public int ClassTypeId { get; set; }
    /// <summary>
    /// Gets or sets the seat capacity of the flight class.
    /// </summary>
    public int SeatCapacity { get; set; }
    /// <summary>
    /// Gets or sets the price of the flight class.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the flight associated with this flight class.
    /// </summary>
    public required Flight Flight { get; set; }
    /// <summary>
    /// Gets or sets the class type associated with this flight class.
    /// </summary>
    public required ClassType ClassType { get; set; }
}
