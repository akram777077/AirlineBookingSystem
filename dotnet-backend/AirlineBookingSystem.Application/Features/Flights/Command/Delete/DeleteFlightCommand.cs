using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Command.Delete;

public record DeleteFlightCommand(int Id) : IRequest<Result>;
