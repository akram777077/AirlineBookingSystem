using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetById;

public record GetFlightByIdQuery (int Id) : IRequest<Result<FlightDetailsDto>>;