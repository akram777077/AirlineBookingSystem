using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Query.ById;

public record GetFlightByIdCommand (int Id) : IRequest<Result<FlightDetailsDto>>;