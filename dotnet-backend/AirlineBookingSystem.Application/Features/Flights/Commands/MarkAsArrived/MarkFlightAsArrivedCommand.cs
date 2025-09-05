using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsArrived;

/// <summary>
/// Represents a command to mark a flight as arrived.
/// </summary>
/// <param name="Id">The unique identifier of the flight to mark as arrived.</param>
public record MarkFlightAsArrivedCommand(int Id) : IRequest<Result>;
