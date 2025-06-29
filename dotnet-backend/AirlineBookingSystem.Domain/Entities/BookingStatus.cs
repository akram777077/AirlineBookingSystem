using System.Collections.Generic;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Domain.Entities
{
    public class BookingStatus
    {
        public int Id { get; set; }
        public required string BookingStatusName { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
