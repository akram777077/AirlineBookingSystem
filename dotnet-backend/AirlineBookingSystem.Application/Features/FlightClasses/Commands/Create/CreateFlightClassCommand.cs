using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;

public record CreateFlightClassCommand(CreateFlightClassDto CreateFlightClassDto) : IRequest<Result<int>>;

