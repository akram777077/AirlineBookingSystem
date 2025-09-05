using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;

/// <summary>
/// Represents a command to update an existing airplane.
/// </summary>
/// <param name="Id">The unique identifier of the airplane to update.</param>
/// <param name="UpdateAirplaneDto">The data transfer object containing the updated details for the airplane.</param>
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;

/// <summary>
/// Represents a command to update an airplane.
/// </summary>
public class UpdateAirplaneCommand : IRequest
{
    /// <summary>
    /// Gets or sets the ID of the airplane to update.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the model of the airplane.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer of the airplane.
    /// </summary>
    public string Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the capacity of the airplane.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Gets or sets the code of the airplane.
    /// </summary>
    public string Code { get; set; }
}