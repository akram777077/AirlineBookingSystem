using AirlineBookingSystem.Shared.DTOs.Seats;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Update;

public record UpdateSeatCommand(int Id, UpdateSeatDto Seat) : IRequest<Result>;