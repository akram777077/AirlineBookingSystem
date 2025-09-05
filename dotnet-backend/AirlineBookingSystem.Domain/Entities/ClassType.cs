using System.Collections.Generic;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a class type entity.
/// </summary>
public class ClassType
{
    /// <summary>
    /// Gets or sets the ID of the class type.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the class type.
    /// </summary>
    public ClassTypeEnum Name { get; set; }
    /// <summary>
    /// Gets or sets the collection of flight classes associated with this class type.
    /// </summary>
    public ICollection<FlightClass> FlightClasses { get; set; } = new List<FlightClass>();
    /// <summary>
    /// Gets or sets the collection of seats associated with this class type.
    /// </summary>
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
