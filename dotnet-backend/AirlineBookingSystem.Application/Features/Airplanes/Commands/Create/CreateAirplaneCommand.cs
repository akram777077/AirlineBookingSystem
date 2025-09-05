using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;

/// <summary>
/// Represents a command to create a new airplane.
/// </summary>
public class CreateAirplaneCommand : IRequest<AirplaneDto>
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