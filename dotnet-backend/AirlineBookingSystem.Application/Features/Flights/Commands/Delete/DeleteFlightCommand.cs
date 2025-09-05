using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Delete;

/// <summary>
/// Represents a command to delete a flight by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the flight to delete.</param>
public record DeleteFlightCommand(int Id) : IRequest<Result>;
