using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetById;

/// <summary>
/// Handles the retrieval of a booking status by its unique identifier.
/// </summary>
public class GetBookingStatusByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetBookingStatusByIdQuery, Result<BookingStatusDto>>
{
    /// <summary>
    /// Handles the <see cref="GetBookingStatusByIdQuery"/> to retrieve a booking status by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{BookingStatusDto}"/> indicating the success or failure of the operation, with the booking status DTO on success.</returns>
    public async Task<Result<BookingStatusDto>> Handle(GetBookingStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var bookingStatus = await unitOfWork.BookingStatuses.GetByIdAsync(request.Id);
        return bookingStatus == null ? Result<BookingStatusDto>.NotFound("Booking status not found.") : Result<BookingStatusDto>.Success(mapper.Map<BookingStatusDto>(bookingStatus));
    }
}
