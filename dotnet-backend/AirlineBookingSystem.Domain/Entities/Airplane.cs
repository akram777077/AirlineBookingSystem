using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents an airplane entity.
/// </summary>
public class Airplane
{
    /// <summary>
    /// Gets or sets the ID of the airplane.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the model of the airplane.
    /// </summary>
    public required string Model { get; set; }
    /// <summary>
    /// Gets or sets the manufacturer of the airplane.
    /// </summary>
    public required string Manufacturer { get; set; }
    /// <summary>
    /// Gets or sets the capacity of the airplane.
    /// </summary>
    public int Capacity { get; set; }
    /// <summary>
    /// Gets or sets the code of the airplane.
    /// </summary>
    public required string Code { get; set; }
    /// <summary>
    /// Gets or sets the collection of flights associated with this airplane.
    /// </summary>
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    /// <summary>
    /// Gets or sets the collection of seats associated with this airplane.
    /// </summary>
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
