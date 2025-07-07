using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Command.MarkAsArrived;

public record MarkFlightAsArrivedCommand(int Id) : IRequest<Result>;
