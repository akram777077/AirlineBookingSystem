using System;
using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a flight entity.
/// </summary>
public class Flight
{
    /// <summary>
    /// Gets or sets the ID of the flight.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the flight number.
    /// </summary>
    public required string FlightNumber { get; set; }
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
    public int ArrivalGateId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the departure gate for the flight.
    /// </summary>
    public int DepartureGateId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the flight status.
    /// </summary>
    public int FlightStatusId { get; set; }
    /// <summary>
    /// Gets or sets the creation timestamp of the flight.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Gets or sets the update timestamp of the flight.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    /// <summary>
    /// Gets or sets the deletion timestamp of the flight.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
    /// <summary>
    /// Gets or sets the airplane for the flight.
    /// </summary>
    public required Airplane Airplane { get; set; }
    /// <summary>
    /// Gets or sets the arrival gate for the flight.
    /// </summary>
    public required Gate ArrivalGate { get; set; }
    /// <summary>
    /// Gets or sets the departure gate for the flight.
    /// </summary>
    public required Gate DepartureGate { get; set; }
    /// <summary>
    /// Gets or sets the flight status.
    /// </summary>
    public required FlightStatus FlightStatus { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of flight classes for this flight.
    /// </summary>
    public ICollection<FlightClass> FlightClasses { get; set; } = new List<FlightClass>();
}
