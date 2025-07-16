using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;

public class GetBookingStatusByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetBookingStatusByIdQuery, Result<BookingStatusDto>>
{
    public async Task<Result<BookingStatusDto>> Handle(GetBookingStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var bookingStatus = await unitOfWork.BookingStatuses.GetByIdAsync(request.Id);
        return bookingStatus == null ? Result<BookingStatusDto>.NotFound("Booking status not found.") : Result<BookingStatusDto>.Success(mapper.Map<BookingStatusDto>(bookingStatus));
    }
}
