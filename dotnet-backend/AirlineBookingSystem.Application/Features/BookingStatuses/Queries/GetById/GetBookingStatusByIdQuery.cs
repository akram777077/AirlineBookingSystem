using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;

public class GetBookingStatusByIdQuery(int id) : IRequest<Result<BookingStatusDto>>
{
    public int Id => id;
}
