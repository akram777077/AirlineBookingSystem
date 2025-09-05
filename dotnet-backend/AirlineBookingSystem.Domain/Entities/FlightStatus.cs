using System.Collections.Generic;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a flight status entity.
/// </summary>
public class FlightStatus
{
    /// <summary>
    /// Gets or sets the ID of the flight status.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the flight status.
    /// </summary>
    public FlightStatusEnum StatusName { get; set; }
    /// <summary>
    /// Gets or sets the collection of flights with this status.
    /// </summary>
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
