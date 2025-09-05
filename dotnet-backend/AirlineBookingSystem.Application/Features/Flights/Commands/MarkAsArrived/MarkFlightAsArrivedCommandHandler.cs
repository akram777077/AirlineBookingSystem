using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsArrived;

/// <summary>
/// Handles the command to mark a flight as arrived.
/// </summary>
public class MarkFlightAsArrivedCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkFlightAsArrivedCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="MarkFlightAsArrivedCommand"/> to mark a flight as arrived.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
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