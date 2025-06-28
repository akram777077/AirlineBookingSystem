using AirlineBookingSystem.Shared.DTOs.Flights;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetByDate;

public record GetUpcomingFlightsQuery(DateTime DateTime) : IRequest<IReadOnlyCollection<FlightDto>>;