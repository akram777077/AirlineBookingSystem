using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Update;

/// <summary>
/// Handles the update of an existing seat.
/// </summary>
public class UpdateSeatCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateSeatCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="UpdateSeatCommand"/> to update an existing seat.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
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