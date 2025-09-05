using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a seat by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the seat.</param>
public record GetSeatByIdQuery(int Id) : IRequest<Result<SeatDto>>;