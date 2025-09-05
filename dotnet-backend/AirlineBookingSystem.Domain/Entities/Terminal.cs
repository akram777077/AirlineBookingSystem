using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a terminal entity.
/// </summary>
public class Terminal
{
    /// <summary>
    /// Gets or sets the ID of the terminal.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the terminal.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Gets or sets the ID of the airport where the terminal is located.
    /// </summary>
    public int AirportId { get; set; }
    /// <summary>
    /// Gets or sets the airport where the terminal is located.
    /// </summary>
    public required Airport Airport { get; set; }
    /// <summary>
    /// Gets or sets the collection of gates in this terminal.
    /// </summary>
    public ICollection<Gate> Gates { get; set; } = new List<Gate>();
}
