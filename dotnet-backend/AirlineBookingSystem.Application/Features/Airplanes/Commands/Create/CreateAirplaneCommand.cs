using AirlineBookingSystem.Shared.DTOs.airplanes;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Create;

/// <summary>
/// Represents a command to create a new airplane.
/// </summary>
using AirlineBookingSystem.Shared.Results;

public class CreateAirplaneCommand : IRequest<Result<AirplaneDto>>
{
    public CreateAirplaneCommand(string model, string manufacturer, int capacity, string code)
    {
        Model = model;
        Manufacturer = manufacturer;
        Capacity = capacity;
        Code = code;
    }

    public CreateAirplaneCommand(CreateAirplaneDto dto)
    {
        Model = dto.Model;
        Manufacturer = dto.Manufacturer;
        Capacity = dto.Capacity;
        Code = dto.Code;
    }
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