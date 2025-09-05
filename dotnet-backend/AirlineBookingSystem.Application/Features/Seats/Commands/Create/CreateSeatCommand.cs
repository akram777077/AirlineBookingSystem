using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Create;

/// <summary>
/// Represents a command to create a new seat.
/// </summary>
/// <param name="Seat">The data transfer object containing the details for the new seat.</param>
public record CreateSeatCommand(CreateSeatDto Seat) : IRequest<Result<int>>;