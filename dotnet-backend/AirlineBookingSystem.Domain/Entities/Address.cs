using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public required string Street { get; set; }
        public required string ZipCode { get; set; }

        public int CityId { get; set; }
        public required City City { get; set; }
        public ICollection<Person> People { get; set; } = new List<Person>();
    }
}
