using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Update;

public record UpdateFlightCommand(
    int Id,
    string FlightNumber,
    int FromAirportId,
    int ToAirportId,
    DateTime DepartureTime,
    DateTime ArrivalTime
) : IRequest<bool>; // Returns true if updated
