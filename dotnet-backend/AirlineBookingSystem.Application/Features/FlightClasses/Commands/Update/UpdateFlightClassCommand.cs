using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;

public record UpdateFlightClassCommand(UpdateFlightClassDto UpdateFlightClassDto) : IRequest<Result<int>>;