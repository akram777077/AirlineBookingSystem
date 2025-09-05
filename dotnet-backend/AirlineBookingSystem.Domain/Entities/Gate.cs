using System.Collections.Generic;

namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a gate entity.
/// </summary>
public class Gate
{
    /// <summary>
    /// Gets or sets the ID of the gate.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the gate number.
    /// </summary>
    public required string GateNumber { get; set; }
    /// <summary>
    /// Gets or sets the ID of the terminal where the gate is located.
    /// </summary>
    public int TerminalId { get; set; }
    /// <summary>
    /// Gets or sets the terminal where the gate is located.
    /// </summary>
    public required Terminal Terminal { get; set; }
    /// <summary>
    /// Gets or sets the collection of departure flights from this gate.
    /// </summary>
    public ICollection<Flight> DepartureFlights { get; set; } = new List<Flight>();
    /// <summary>
    /// Gets or sets the collection of arrival flights at this gate.
    /// </summary>
    public ICollection<Flight> ArrivalFlights { get; set; } = new List<Flight>();
}
