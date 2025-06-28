using AirlineBookingSystem.Shared.DTOs.Flights;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetUpcomingFlightsByDateTime;

public record GetUpcomingFlightsByDateTimeQuery(DateTime DateTime) : IRequest<IReadOnlyCollection<FlightDto>>;