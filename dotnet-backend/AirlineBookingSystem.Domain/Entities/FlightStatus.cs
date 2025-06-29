using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class FlightStatus
{
    public int Id { get; set; }
    public required string StatusName { get; set; }
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
