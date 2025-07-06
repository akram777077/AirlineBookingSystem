using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Command.Update;

public record UpdateFlightCommand(int Id, UpdateFlightDto Dto) : IRequest<Result>;
