using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Create;

/// <summary>
/// Handles the creation of a new seat.
/// </summary>
public class CreateSeatCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateSeatCommand, Result<int>>
{
    /// <summary>
    /// Handles the <see cref="CreateSeatCommand"/> to create a new seat.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{Int32}"/> indicating the success or failure of the operation, with the ID of the created seat on success.</returns>
    public async Task<Result<int>> Handle(CreateSeatCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var seat = mapper.Map<Seat>(request.Seat);
            await unitOfWork.Seats.AddAsync(seat);
            await unitOfWork.CompleteAsync();
            return Result<int>.Success(seat.Id);
        }
        catch (Exception e)
        {
            return Result<int>.Failure(e.Message);
        }
    }
}