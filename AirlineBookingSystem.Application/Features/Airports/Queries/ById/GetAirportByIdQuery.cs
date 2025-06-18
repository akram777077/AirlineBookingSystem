using AirlineBookingSystem.Shared.DTOs.Airports;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.ById;

public record GetAirportByIdQuery(int Id) : IRequest<AirportDto?>;