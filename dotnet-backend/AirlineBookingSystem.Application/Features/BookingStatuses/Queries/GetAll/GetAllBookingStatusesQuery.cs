using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;

public record GetAllBookingStatusesQuery : IRequest<Result<IEnumerable<BookingStatusDto>>>;
