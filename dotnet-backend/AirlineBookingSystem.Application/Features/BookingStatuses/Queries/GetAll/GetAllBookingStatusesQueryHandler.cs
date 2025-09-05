using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.BookingStatuses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.BookingStatuses.Queries.GetAll;

/// <summary>
/// Handles the retrieval of all booking statuses.
/// </summary>
public class GetAllBookingStatusesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllBookingStatusesQuery, Result<IEnumerable<BookingStatusDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetAllBookingStatusesQuery"/> to retrieve all booking statuses.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{IEnumerable{BookingStatusDto}}"/> containing a list of booking status DTOs on success.</returns>
    public async Task<Result<IEnumerable<BookingStatusDto>>> Handle(GetAllBookingStatusesQuery request, CancellationToken cancellationToken)
    {
        var bookingStatuses = await unitOfWork.BookingStatuses.GetAllAsync();
        return Result<IEnumerable<BookingStatusDto>>.Success(mapper.Map<IEnumerable<BookingStatusDto>>(bookingStatuses));
    }
}
