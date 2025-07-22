using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class Airplane
{
    public int Id { get; set; }
    public required string Model { get; set; }
    public required string Manufacturer { get; set; }
    public int Capacity { get; set; }
    public required string Code { get; set; }
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
