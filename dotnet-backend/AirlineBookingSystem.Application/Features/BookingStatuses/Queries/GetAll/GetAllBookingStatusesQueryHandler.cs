using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;

public class GetAllBookingStatusesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllBookingStatusesQuery, Result<IEnumerable<BookingStatusDto>>>
{
    public async Task<Result<IEnumerable<BookingStatusDto>>> Handle(GetAllBookingStatusesQuery request, CancellationToken cancellationToken)
    {
        var bookingStatuses = await unitOfWork.BookingStatuses.GetAllAsync();
        return Result<IEnumerable<BookingStatusDto>>.Success(mapper.Map<IEnumerable<BookingStatusDto>>(bookingStatuses));
    }
}
