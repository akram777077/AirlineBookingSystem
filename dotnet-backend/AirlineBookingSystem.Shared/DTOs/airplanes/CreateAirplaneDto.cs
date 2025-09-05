
namespace AirlineBookingSystem.Shared.DTOs.airplanes;

/// <summary>
/// Represents a data transfer object for creating an airplane.
/// </summary>
public struct CreateAirplaneDto
{
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
