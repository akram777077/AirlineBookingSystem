using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsArrived;

public class MarkFlightAsArrivedCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkFlightAsArrivedCommand, Result>
{
    public async Task<Result> Handle(MarkFlightAsArrivedCommand request, CancellationToken cancellationToken)
    {
        var flight = await unitOfWork.Flights.GetByIdAsync(request.Id, true);
        if (flight == null)
            return Result.NotFound("Flight not found.");

        if (flight.FlightStatus.StatusName != FlightStatusEnum.Departed)
            return Result.Failure("Flight must be departed to be marked as arrived.", ResultStatusCode.BadRequest);

        flight.FlightStatusId = (int)FlightStatusEnum.Arrived;
        flight.FlightStatus = new FlightStatus
        {
            Id = (int)FlightStatusEnum.Arrived,
            StatusName = FlightStatusEnum.Arrived
        };

        unitOfWork.Flights.Update(flight);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.NoContent);
    }
}