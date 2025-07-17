using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Update;

public record UpdateFlightCommand(int Id, UpdateFlightDto Dto) : IRequest<Result>;
