using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;

/// <summary>
/// Handles the retrieval of available seats based on a filter.
/// </summary>
public class GetAvailableSeatsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAvailableSeatsQuery, Result<List<SeatDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetAvailableSeatsQuery"/> to retrieve available seats.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{List{SeatDto}}"/> containing a list of available seat DTOs on success.</returns>
    public async Task<Result<List<SeatDto>>> Handle(GetAvailableSeatsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var flight = await unitOfWork.Flights.GetByIdAsync(request.Filter.FlightId);
            if (flight == null)
            {
                return Result.NotFound<List<SeatDto>>($"Flight with ID {request.Filter.FlightId} not found.");
            }

            if (request.Filter.ClassTypeId.HasValue)
            {
                var classType = await unitOfWork.ClassTypes.GetByIdAsync(request.Filter.ClassTypeId.Value);
                if (classType == null)
                {
                    return Result.NotFound<List<SeatDto>>($"ClassType with ID {request.Filter.ClassTypeId.Value} not found.");
                }
            }

            var seats = await unitOfWork.Seats.GetAvailableSeatsAsync(request.Filter.FlightId, request.Filter.ClassTypeId);
            var seatDtos = mapper.Map<List<SeatDto>>(seats);
            return Result.Success(seatDtos);
        }
        catch (Exception e)
        {
            return Result.Failure<List<SeatDto>>(e.Message);
        }
    }
}