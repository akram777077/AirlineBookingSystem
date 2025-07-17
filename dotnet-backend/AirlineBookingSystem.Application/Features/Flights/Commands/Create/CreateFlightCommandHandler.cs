using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Create;

public class CreateFlightCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateFlightCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        Airplane? airplane = await unitOfWork.Airplanes.GetByIdAsync(request.Dto.AirplaneId);
        if (airplane == null)
            return Result<int>.Failure("Airplane not found", ResultStatusCode.NotFound);

        Gate? departureGate = await unitOfWork.Gates.GetByIdAsync(request.Dto.DepartureGateId);
        if (departureGate == null)
            return Result<int>.Failure("Departure gate not found", ResultStatusCode.NotFound);

        Gate? arrivalGate = null;
        if (request.Dto.ArrivalGateId.HasValue)
        {
            arrivalGate = await unitOfWork.Gates.GetByIdAsync(request.Dto.ArrivalGateId.Value);
            if (arrivalGate == null)
                return Result<int>.Failure("Arrival gate not found", ResultStatusCode.NotFound);
        }
        

        var flight = mapper.Map<Flight>(request.Dto);
        flight.FlightNumber = await GenerateUniqueFlightNumberAsync();
        flight.Airplane = airplane;
        flight.DepartureGate = departureGate;
        flight.ArrivalGate = arrivalGate;
        flight.FlightStatusId = (int)FlightStatusEnum.Scheduled;


        await unitOfWork.Flights.AddAsync(flight);
        await unitOfWork.CompleteAsync();
        return Result<int>.Success(flight.Id,ResultStatusCode.Created);
    }

    private async Task<string> GenerateUniqueFlightNumberAsync()
    {
        string flightNumber;
        do
        {
            flightNumber = GenerateRandomFlightNumber();
        }
        while (await unitOfWork.Flights.IsFlightNumberExistsAsync(flightNumber));

        return flightNumber;
    }

    private string GenerateRandomFlightNumber()
    {
        var random = new Random();
        return $"FL{random.Next(100, 1000)}";
    }
}