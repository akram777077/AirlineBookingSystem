using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class FlightClass
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int ClassTypeId { get; set; }
    public int SeatCapacity { get; set; }
    public decimal Price { get; set; }
    public required Flight Flight { get; set; }
    public required ClassType ClassType { get; set; }
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}
