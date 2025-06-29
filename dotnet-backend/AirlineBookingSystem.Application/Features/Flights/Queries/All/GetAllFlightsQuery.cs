using AirlineBookingSystem.Shared.DTOs.Flights;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.All;

public record GetAllFlightsQuery : IRequest<IReadOnlyCollection<FlightDto>>;