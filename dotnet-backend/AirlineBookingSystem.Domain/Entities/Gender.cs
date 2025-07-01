using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class Gender
{
    public int Id { get; set; }
    public required char Code { get; set; }
    public ICollection<Person> People { get; set; } = new List<Person>();
}
