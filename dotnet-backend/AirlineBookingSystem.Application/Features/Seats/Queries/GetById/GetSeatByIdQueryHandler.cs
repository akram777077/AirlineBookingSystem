using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetById;

/// <summary>
/// Handles the retrieval of a seat by its unique identifier.
/// </summary>
public class GetSeatByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetSeatByIdQuery, Result<SeatDto>>
{
    /// <summary>
    /// Handles the <see cref="GetSeatByIdQuery"/> to retrieve a seat by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{SeatDto}"/> indicating the success or failure of the operation, with the seat DTO on success.</returns>
    public async Task<Result<SeatDto>> Handle(GetSeatByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var seat = await unitOfWork.Seats.GetByIdAsync(request.Id);
            if (seat == null)
            {
                return Result.NotFound<SeatDto>("Seat not found.");
            }
            var seatDto = mapper.Map<SeatDto>(seat);
            return Result.Success(seatDto);
        }
        catch (Exception e)
        {
            return Result.Failure<SeatDto>(e.Message);
        }
    }
}