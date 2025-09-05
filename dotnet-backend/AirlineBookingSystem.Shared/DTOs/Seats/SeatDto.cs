namespace AirlineBookingSystem.Shared.DTOs;

/// <summary>
/// Represents a seat data transfer object.
/// </summary>
public class SeatDto
{
    /// <summary>
    /// Gets or sets the ID of the seat.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the ID of the class type for the seat.
    /// </summary>
    public int ClassTypesId { get; set; }
    /// <summary>
    /// Gets or sets the seat number.
    /// </summary>
    public required string SeatNumber { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the seat is reserved.
    /// </summary>
    public bool IsReserved { get; set; }
    /// <summary>
    /// Gets or sets the ID of the airplane where the seat is located.
    /// </summary>
    public int AirplaneId { get; set; }
}