using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities
{
    /// <summary>
    /// Represents a country entity.
    /// </summary>
    public class Country 
    {
        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Gets or sets the code of the country.
        /// </summary>
        public required string Code { get; set; }
        /// <summary>
        /// Gets or sets the collection of cities in this country.
        /// </summary>
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
