using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Seats.Commands.Create;

public class CreateSeatCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateSeatCommand, Result<int>>
{
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