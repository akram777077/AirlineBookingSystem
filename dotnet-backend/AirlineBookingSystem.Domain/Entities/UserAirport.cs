namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a user-airport association entity.
/// </summary>
public class UserAirport
{
    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Gets or sets the ID of the airport.
    /// </summary>
    public int AirportId { get; set; }
    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public required User User { get; set; }
    /// <summary>
    /// Gets or sets the airport.
    /// </summary>
    public required Airport Airport { get; set; }
}
