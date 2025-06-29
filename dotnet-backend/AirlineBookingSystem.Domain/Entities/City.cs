using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public int CountryId { get; set; }
        public required Country Country { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Airport> Airports { get; set; } = new List<Airport>();
    }
}
