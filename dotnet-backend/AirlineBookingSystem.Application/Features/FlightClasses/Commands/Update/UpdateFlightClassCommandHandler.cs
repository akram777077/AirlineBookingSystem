using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;

public class UpdateFlightClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateFlightClassCommand, Result<int>>
{
    public async Task<Result<int>> Handle(UpdateFlightClassCommand request, CancellationToken cancellationToken)
    {
        var flightClass = await unitOfWork.FlightClasses.GetByIdAsync(request.UpdateFlightClassDto.Id);
        if (flightClass == null)
            return Result<int>.Failure("FlightClass not found", ResultStatusCode.NotFound);

        mapper.Map(request.UpdateFlightClassDto, flightClass);
        unitOfWork.FlightClasses.Update(flightClass);
        await unitOfWork.CompleteAsync();
        return Result<int>.Success(flightClass.Id, ResultStatusCode.Success);
    }
}