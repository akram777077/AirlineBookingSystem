namespace AirlineBookingSystem.Shared.DTOs.BookingStatuses;

/// <summary>
/// Represents a booking status data transfer object.
/// </summary>
public struct BookingStatusDto
{
    /// <summary>
    /// Gets or sets the ID of the booking status.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the booking status.
    /// </summary>
    public required string Name { get; set; }
}
