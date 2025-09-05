using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities
{
    /// <summary>
    /// Represents a city entity.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Gets or sets the ID of the city.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the country where the city is located.
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// Gets or sets the country where the city is located.
        /// </summary>
        public required Country Country { get; set; }

        /// <summary>
        /// Gets or sets the collection of addresses in this city.
        /// </summary>
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        /// <summary>
        /// Gets or sets the collection of airports in this city.
        /// </summary>
        public ICollection<Airport> Airports { get; set; } = new List<Airport>();
    }
}
