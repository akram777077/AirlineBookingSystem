using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a gender entity.
/// </summary>
public class Gender
{
    /// <summary>
    /// Gets or sets the ID of the gender.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the code of the gender.
    /// </summary>
    public required char Code { get; set; }
    /// <summary>
    /// Gets or sets the collection of people with this gender.
    /// </summary>
    public ICollection<Person> People { get; set; } = new List<Person>();
}
