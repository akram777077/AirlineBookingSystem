using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Create;

public record CreateSeatCommand(CreateSeatDto Seat) : IRequest<Result<int>>;