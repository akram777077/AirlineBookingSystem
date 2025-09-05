using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities
{
    /// <summary>
    /// Represents an address entity.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the ID of the address.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        public required string Street { get; set; }
        /// <summary>
        /// Gets or sets the zip code of the address.
        /// </summary>
        public required string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the ID of the city.
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public required City City { get; set; }
        /// <summary>
        /// Gets or sets the collection of people associated with this address.
        /// </summary>
        public ICollection<Person> People { get; set; } = new List<Person>();
    }
}
