using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Commands.Update;

public record UpdateAirportCommand(UpdateAirportDto Airport) : IRequest<Result<AirportDto>>;