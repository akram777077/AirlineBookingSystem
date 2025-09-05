using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;

/// <summary>
/// Represents a command to create a new flight class.
/// </summary>
/// <param name="CreateFlightClassDto">The data transfer object containing the details for the new flight class.</param>
public record CreateFlightClassCommand(CreateFlightClassDto CreateFlightClassDto) : IRequest<Result<int>>;

