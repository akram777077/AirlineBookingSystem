namespace AirlineBookingSystem.Shared.DTOs.ClassTypes;

/// <summary>
/// Represents a class type data transfer object.
/// </summary>
public struct ClassTypeDto
{
    /// <summary>
    /// Gets or sets the ID of the class type.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the class type.
    /// </summary>
    public required string Name { get; set; }
}
