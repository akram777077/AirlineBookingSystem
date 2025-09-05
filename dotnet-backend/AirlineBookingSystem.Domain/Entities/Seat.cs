namespace AirlineBookingSystem.Domain.Entities;

/// <summary>
/// Represents a seat entity.
/// </summary>
public class Seat
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
    public bool IsReserved { get; set; } = false;
    /// <summary>
    /// Gets or sets the ID of the airplane where the seat is located.
    /// </summary>
    public int AirplaneId { get; set; }

    /// <summary>
    /// Gets or sets the class type of the seat.
    /// </summary>
    public required ClassType ClassType { get; set; }
    /// <summary>
    /// Gets or sets the airplane where the seat is located.
    /// </summary>
    public required Airplane Airplane { get; set; }
    
}
