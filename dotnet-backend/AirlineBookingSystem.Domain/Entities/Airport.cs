using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities
{
    public class Airport
    {
        public int Id { get; set; }
        public required string AirportCode { get; set; }
        public required string Name { get; set; }
        public int CityId { get; set; }
        public required string Timezone { get; set; }
        public required City City { get; set; }
        public ICollection<Terminal> Terminals { get; set; } = new List<Terminal>();
        public ICollection<UserAirport> UserAirports { get; set; } = new List<UserAirport>();
    }
}
