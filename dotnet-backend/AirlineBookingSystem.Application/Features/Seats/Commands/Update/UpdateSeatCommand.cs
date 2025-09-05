using AirlineBookingSystem.Shared.DTOs.Seats;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Update;

/// <summary>
/// Represents a command to update an existing seat.
/// </summary>
/// <param name="Id">The unique identifier of the seat to update.</param>
/// <param name="Seat">The data transfer object containing the updated details for the seat.</param>
public record UpdateSeatCommand(int Id, UpdateSeatDto Seat) : IRequest<Result>;