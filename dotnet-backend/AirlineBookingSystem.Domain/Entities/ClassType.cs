using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class ClassType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<FlightClass> FlightClasses { get; set; } = new List<FlightClass>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}
