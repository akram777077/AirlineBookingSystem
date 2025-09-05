using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities
{
    /// <summary>
    /// Represents an airport entity.
    /// </summary>
    public class Airport
    {
        /// <summary>
        /// Gets or sets the ID of the airport.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the code of the airport.
        /// </summary>
        public required string AirportCode { get; set; }
        /// <summary>
        /// Gets or sets the name of the airport.
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Gets or sets the ID of the city where the airport is located.
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// Gets or sets the timezone of the airport.
        /// </summary>
        public required string Timezone { get; set; }
        /// <summary>
        /// Gets or sets the city where the airport is located.
        /// </summary>
        public required City City { get; set; }
        /// <summary>
        /// Gets or sets the collection of terminals in this airport.
        /// </summary>
        public ICollection<Terminal> Terminals { get; set; } = new List<Terminal>();
        /// <summary>
        /// Gets or sets the collection of user-airport associations.
        /// </summary>
        public ICollection<UserAirport> UserAirports { get; set; } = new List<UserAirport>();
    }
}
