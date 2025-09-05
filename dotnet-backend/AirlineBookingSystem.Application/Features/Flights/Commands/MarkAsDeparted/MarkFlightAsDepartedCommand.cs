using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsDeparted;

/// <summary>
/// Represents a command to mark a flight as departed.
/// </summary>
/// <param name="Id">The unique identifier of the flight to mark as departed.</param>
public record MarkFlightAsDepartedCommand(int Id) : IRequest<Result>;
