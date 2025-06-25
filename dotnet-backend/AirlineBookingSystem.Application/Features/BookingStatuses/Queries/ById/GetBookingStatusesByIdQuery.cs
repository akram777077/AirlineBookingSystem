using AirlineBookingSystem.Shared.DTOs.BookingStatus;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.ById;

public record GetBookingStatusesByIdQuery(int Id) : IRequest<BookingStatusDto?>;