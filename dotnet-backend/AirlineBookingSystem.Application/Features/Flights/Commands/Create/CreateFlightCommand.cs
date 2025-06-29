using AirlineBookingSystem.Shared.DTOs.Flights;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Create;

public record CreateFlightCommand(
    string FlightNumber,
    int FromAirportId,
    int ToAirportId,
    DateTime DepartureTime,
    DateTime ArrivalTime,
    decimal Price
) : IRequest<int>; // Returns the created Flight Id
