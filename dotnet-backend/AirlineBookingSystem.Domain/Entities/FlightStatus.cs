using System.Collections.Generic;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities;

public class FlightStatus
{
    public int Id { get; set; }
    public FlightStatusEnum StatusName { get; set; }
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
