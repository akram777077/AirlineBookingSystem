using System;
using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class Flight
{
    public int Id { get; set; }
    public required string FlightNumber { get; set; }
    public DateTimeOffset DepartureTime { get; set; }
    public DateTimeOffset? ArrivalTime { get; set; }
    public int AirplaneId { get; set; }
    public int ArrivalGateId { get; set; }
    public int DepartureGateId { get; set; }
    public int FlightStatusId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required Airplane Airplane { get; set; }
    public required Gate ArrivalGate { get; set; }
    public required Gate DepartureGate { get; set; }
    public required FlightStatus FlightStatus { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<FlightClass> FlightClasses { get; set; } = new List<FlightClass>();
}
