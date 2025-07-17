using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.MarkAsDeparted;

public class MarkFlightAsDepartedCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkFlightAsDepartedCommand, Result>
{
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