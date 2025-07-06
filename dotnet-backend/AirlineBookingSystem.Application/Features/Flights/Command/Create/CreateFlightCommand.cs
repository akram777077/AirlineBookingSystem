using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Command.Create;

public record CreateFlightCommand(CreateFlightDto Dto) : IRequest<Result<int>>;
