namespace AirlineBookingSystem.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public required string SeatNumber { get; set; }

        public int PassengerId { get; set; }
        public required User Passenger { get; set; } 

        public int FlightId { get; set; }
        public required Flight Flight { get; set; }

        public int BookingStatusId { get; set; }
        public required BookingStatus BookingStatus { get; set; }
        
    }
}
