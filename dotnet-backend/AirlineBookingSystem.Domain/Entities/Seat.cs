using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

public class Seat
{
    public int Id { get; set; }
    public int FlightClassId { get; set; }
    public required string SeatNumber { get; set; }
    public bool IsReserved { get; set; } = false;
    public required FlightClass FlightClass { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
