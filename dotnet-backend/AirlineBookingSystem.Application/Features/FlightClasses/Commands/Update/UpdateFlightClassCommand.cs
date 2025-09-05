using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;

/// <summary>
/// Represents a command to update an existing flight class.
/// </summary>
/// <param name="UpdateFlightClassDto">The data transfer object containing the updated details for the flight class.</param>
public record UpdateFlightClassCommand(UpdateFlightClassDto UpdateFlightClassDto) : IRequest<Result<int>>;