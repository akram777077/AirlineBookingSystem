using AirlineBookingSystem.Shared.DTOs.Airports;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.ByCode;

public record GetAirportByCodeQuery(string Code) : IRequest<AirportDto?>;