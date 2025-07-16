using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Query.ById;

public record GetAirportByIdQuery(int Id) : IRequest<Result<AirportDto>>;