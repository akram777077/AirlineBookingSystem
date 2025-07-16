using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;

public record GetBookingStatusByIdQuery(int Id) : IRequest<Result<BookingStatusDto>>;