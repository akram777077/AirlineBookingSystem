using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsDeparted;

/// <summary>
/// Handles the command to mark a flight as departed.
/// </summary>
public class MarkFlightAsDepartedCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkFlightAsDepartedCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="MarkFlightAsDepartedCommand"/> to mark a flight as departed.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(MarkFlightAsDepartedCommand request, CancellationToken cancellationToken)
    {
        var flight = await unitOfWork.Flights.GetByIdAsync(request.Id);
        if (flight == null)
            return Result.NotFound("Flight not found.");

        flight.FlightStatusId = (int)FlightStatusEnum.Departed;
        flight.FlightStatus = new FlightStatus
        {
            Id = (int)FlightStatusEnum.Departed,
            StatusName = FlightStatusEnum.Departed
        };

        unitOfWork.Flights.Update(flight);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.NoContent);
    }
}