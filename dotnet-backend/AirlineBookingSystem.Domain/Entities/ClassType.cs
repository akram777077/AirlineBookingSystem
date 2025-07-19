using System.Collections.Generic;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities;

public class ClassType
{
    public int Id { get; set; }
    public ClassTypeEnum Name { get; set; }
    public ICollection<FlightClass> FlightClasses { get; set; } = new List<FlightClass>();
}
