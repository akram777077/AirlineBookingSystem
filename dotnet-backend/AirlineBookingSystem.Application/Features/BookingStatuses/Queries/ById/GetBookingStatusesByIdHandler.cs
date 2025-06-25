using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.BookingStatus;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.ById;

public class GetBookingStatusesByIdHandler (IBookingStatusRepository bookingStatusRepository,IMapper mapper)
    : IRequestHandler<GetBookingStatusesByIdQuery, BookingStatusDto?>
{
    public async Task<BookingStatusDto?> Handle(GetBookingStatusesByIdQuery request, CancellationToken cancellationToken)
    {
        var bookingStatus = await bookingStatusRepository.GetByIdAsync(request.Id);
        return mapper.Map<BookingStatusDto>(bookingStatus);
    }
}