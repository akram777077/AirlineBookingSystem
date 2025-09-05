using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Update;

/// <summary>
/// Handles the update of an existing flight.
/// </summary>
public class UpdateFlightCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateFlightCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="UpdateFlightCommand"/> to update an existing flight.
    /// This involves validating associated entities (airplane, gates) and potentially updating the flight status to 'Delayed'
    /// if departure or arrival times are changed to a later time.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await unitOfWork.Flights.GetByIdAsync(request.Id);
        if (flight == null)
            return Result.NotFound("Flight not found");

        Airplane? airplane = await unitOfWork.Airplanes.GetByIdAsync(request.Dto.AirplaneId);
        if (airplane == null)
            return Result.Failure("Airplane not found", ResultStatusCode.NotFound);

        Gate? departureGate = await unitOfWork.Gates.GetByIdAsync(request.Dto.DepartureGateId);
        if (departureGate == null)
            return Result.Failure("Departure gate not found", ResultStatusCode.NotFound);

        Gate? arrivalGate = null;
        if (request.Dto.ArrivalGateId.HasValue)
        {
            arrivalGate = await unitOfWork.Gates.GetByIdAsync(request.Dto.ArrivalGateId.Value);
            if (arrivalGate == null)
                return Result.Failure("Arrival gate not found", ResultStatusCode.NotFound);
        }

        var originalDepartureTime = flight.DepartureTime;
        var originalArrivalTime = flight.ArrivalTime;

        mapper.Map(request.Dto, flight);

        flight.Airplane = airplane;
        flight.DepartureGate = departureGate;
        flight.ArrivalGate = arrivalGate!;
        
        if (request.Dto.DepartureTime > originalDepartureTime ||
            (request.Dto.ArrivalTime.HasValue && originalArrivalTime.HasValue && request.Dto.ArrivalTime.Value > originalArrivalTime.Value))
        {
            flight.FlightStatusId = (int)FlightStatusEnum.Delayed;
            flight.FlightStatus = new FlightStatus
            {
                Id = (int)FlightStatusEnum.Delayed,
                StatusName = FlightStatusEnum.Delayed
            };
        }

        unitOfWork.Flights.Update(flight);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.NoContent);
    }
}