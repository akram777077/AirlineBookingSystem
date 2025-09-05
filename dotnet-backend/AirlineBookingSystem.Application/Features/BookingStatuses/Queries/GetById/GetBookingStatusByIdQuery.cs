using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a booking status by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the booking status.</param>
public record GetBookingStatusByIdQuery(int Id) : IRequest<Result<BookingStatusDto>>;