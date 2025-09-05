using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;

/// <summary>
/// Represents a query to retrieve all booking statuses.
/// </summary>
public record GetAllBookingStatusesQuery : IRequest<Result<IEnumerable<BookingStatusDto>>>;

