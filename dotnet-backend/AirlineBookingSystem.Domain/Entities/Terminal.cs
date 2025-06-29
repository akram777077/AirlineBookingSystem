using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class Terminal
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int AirportId { get; set; }
    public required Airport Airport { get; set; }
    public ICollection<Gate> Gates { get; set; } = new List<Gate>();
}
