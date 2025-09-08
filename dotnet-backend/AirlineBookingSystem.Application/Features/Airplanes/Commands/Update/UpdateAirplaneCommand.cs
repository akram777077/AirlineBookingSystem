
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Commands.Update;

/// <summary>
/// Represents a command to update an airplane.
/// </summary>
using AirlineBookingSystem.Shared.Results;

public class UpdateAirplaneCommand : IRequest<Result<Unit>>
{
    public UpdateAirplaneCommand(int id, string model, string manufacturer, int capacity, string code)
    {
        Id = id;
        Model = model;
        Manufacturer = manufacturer;
        Capacity = capacity;
        Code = code;
    }

    public UpdateAirplaneCommand(int id, UpdateAirplaneDto dto)
    {
        Id = id;
        Model = dto.Model;
        Manufacturer = dto.Manufacturer;
        Capacity = dto.Capacity;
        Code = dto.Code;
    }
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
