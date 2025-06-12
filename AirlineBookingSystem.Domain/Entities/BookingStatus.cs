using AirlineBookingSystem.Domain.Enums;

namespace AirlineBookingSystem.Domain.Entities
{
    public class BookingStatus
    {
        public int Id { get; set; }
        public BookingStatusEnum StatusName { get; set; }
    }
}
