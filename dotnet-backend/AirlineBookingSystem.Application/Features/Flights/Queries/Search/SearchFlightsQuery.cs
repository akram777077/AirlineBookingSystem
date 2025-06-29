using AirlineBookingSystem.Shared.DTOs.Flights;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.Search;

public record SearchFlightsQuery(string FromCode, string ToCode, DateTime DateTime) : IRequest<IReadOnlyCollection<FlightDto>>;