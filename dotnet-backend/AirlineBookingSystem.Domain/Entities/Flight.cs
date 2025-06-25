namespace AirlineBookingSystem.Domain.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public required string FlightNumber { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }

        public int FromAirportId { get; set; }
        public required Airport FromAirport { get; set; }

        public int ToAirportId { get; set; }
        public required Airport ToAirport { get; set; } 
    }
}
