using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Application.Features.Flights.Command.Update;

public class UpdateFlightCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateFlightCommand, Result>
{
    public async Task<Result> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await unitOfWork.Flights.GetByIdAsync(request.Id);
        if (flight == null)
            return Result.NotFound("Flight not found");

        var airplane = await unitOfWork.Airplanes.GetByIdAsync(request.Dto.AirplaneId);
        if (airplane == null)
            return Result.Failure("Airplane not found", ResultStatusCode.NotFound);

        var departureGate = await unitOfWork.Gates.GetByIdAsync(request.Dto.DepartureGateId);
        if (departureGate == null)
            return Result.Failure("Departure gate not found", ResultStatusCode.NotFound);

        Gate arrivalGate = null;
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
        flight.ArrivalGate = arrivalGate;
        
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