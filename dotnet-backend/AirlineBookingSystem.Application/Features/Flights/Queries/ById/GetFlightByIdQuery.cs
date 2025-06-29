using AirlineBookingSystem.Shared.DTOs.Flights;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.ById;

public record GetFlightByIdQuery (int Id) : IRequest<FlightDto?>;