namespace AirlineBookingSystem.Domain.Entities
{
    public class Passenger
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; } 
    }
}
