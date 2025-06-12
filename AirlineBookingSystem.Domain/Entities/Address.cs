namespace AirlineBookingSystem.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public required string Street { get; set; }
        public required string ZipCode { get; set; }

        public int CityId { get; set; }
        public required City City { get; set; }

        public int CountryId { get; set; }
        public required Country Country { get; set; } 
    }
}
