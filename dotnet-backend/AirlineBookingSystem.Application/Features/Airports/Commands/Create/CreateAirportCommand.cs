using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Commands.Create;

public record CreateAirportCommand(CreateAirportDto Airport) : IRequest<Result<AirportDto>>;