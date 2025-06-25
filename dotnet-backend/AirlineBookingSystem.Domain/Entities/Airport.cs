namespace AirlineBookingSystem.Domain.Entities
{
    public class Airport
    {
        public int Id { get; set; }
        public required string AirportCode { get; set; }
        public required string Name { get; set; }

        public int CityId { get; set; }
        public required City City { get; set; }

        public int CountryId { get; set; }
        public required Country Country { get; set; } 
    }
}
