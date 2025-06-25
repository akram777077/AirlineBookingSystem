using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.BookingStatus;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.All;

public class GetAllBookingStatusesHandler (IBookingStatusRepository bookingStatusRepository,IMapper mapper)
    : IRequestHandler<GetAllBookingStatusesQuery, IReadOnlyCollection<BookingStatusDto>>
{
    public async Task<IReadOnlyCollection<BookingStatusDto>> Handle(GetAllBookingStatusesQuery request, CancellationToken cancellationToken)
    {
        var bookingStatuses = await bookingStatusRepository.GetAllAsync();
        return mapper.Map<List<BookingStatusDto>>(bookingStatuses);
    }
}