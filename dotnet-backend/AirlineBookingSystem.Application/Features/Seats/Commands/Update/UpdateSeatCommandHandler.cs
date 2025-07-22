using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Update;

public class UpdateSeatCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateSeatCommand, Result>
{
    public async Task<Result> Handle(UpdateSeatCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var seat = await unitOfWork.Seats.GetByIdAsync(request.Id);
            if (seat == null)
            {
                return Result.NotFound("Seat not found.");
            }

            mapper.Map(request.Seat, seat);
            unitOfWork.Seats.Update(seat);
            await unitOfWork.CompleteAsync();
            return Result.NoContent();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }
}