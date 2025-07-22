using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetAvailableSeats;

public class GetAvailableSeatsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAvailableSeatsQuery, Result<List<SeatDto>>>
{
    public async Task<Result<List<SeatDto>>> Handle(GetAvailableSeatsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var flight = await unitOfWork.Flights.GetByIdAsync(request.Filter.FlightId);
            if (flight == null)
            {
                return Result<List<SeatDto>>.NotFound($"Flight with ID {request.Filter.FlightId} not found.");
            }

            if (request.Filter.ClassTypeId.HasValue)
            {
                var classType = await unitOfWork.ClassTypes.GetByIdAsync(request.Filter.ClassTypeId.Value);
                if (classType == null)
                {
                    return Result<List<SeatDto>>.NotFound($"ClassType with ID {request.Filter.ClassTypeId.Value} not found.");
                }
            }

            var seats = await unitOfWork.Seats.GetAvailableSeatsAsync(request.Filter.FlightId, request.Filter.ClassTypeId);
            var seatDtos = mapper.Map<List<SeatDto>>(seats);
            return Result<List<SeatDto>>.Success(seatDtos);
        }
        catch (Exception e)
        {
            return Result<List<SeatDto>>.Failure(e.Message);
        }
    }
}