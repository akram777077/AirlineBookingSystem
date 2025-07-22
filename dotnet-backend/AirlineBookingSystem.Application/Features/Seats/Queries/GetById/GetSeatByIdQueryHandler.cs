using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Queries.GetById;

public class GetSeatByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetSeatByIdQuery, Result<SeatDto>>
{
    public async Task<Result<SeatDto>> Handle(GetSeatByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var seat = await unitOfWork.Seats.GetByIdAsync(request.Id);
            if (seat == null)
            {
                return Result<SeatDto>.NotFound("Seat not found.");
            }
            var seatDto = mapper.Map<SeatDto>(seat);
            return Result<SeatDto>.Success(seatDto);
        }
        catch (Exception e)
        {
            return Result<SeatDto>.Failure(e.Message);
        }
    }
}