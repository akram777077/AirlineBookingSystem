
namespace AirlineBookingSystem.Shared.DTOs.airplanes;

/// <summary>
/// Represents an airplane data transfer object.
/// </summary>
public struct AirplaneDto
{
    /// <summary>
    /// Gets or sets the ID of the airplane.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the model of the airplane.
    /// </summary>
    public required string Model { get; set; }
    /// <summary>
    /// Gets or sets the manufacturer of the airplane.
    /// </summary>
    public required string Manufacturer { get; set; }
    /// <summary>
    /// Gets or sets the capacity of the airplane.
    /// </summary>
    public int Capacity { get; set; }
    /// <summary>
    /// Gets or sets the code of the airplane.
    /// </summary>
    public required string Code { get; set; }
}
