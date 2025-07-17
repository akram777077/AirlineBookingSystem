using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Delete;

public record DeleteFlightCommand(int Id) : IRequest<Result>;
