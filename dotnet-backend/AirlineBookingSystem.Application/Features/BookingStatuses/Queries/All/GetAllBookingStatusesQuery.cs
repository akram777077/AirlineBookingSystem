using AirlineBookingSystem.Shared.DTOs.BookingStatus;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.All;

public record GetAllBookingStatusesQuery() : IRequest<IReadOnlyCollection<BookingStatusDto>>;