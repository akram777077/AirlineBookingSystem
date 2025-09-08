using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Create;

/// <summary>
/// Handles the creation of a new flight.
/// </summary>
public class CreateFlightCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateFlightCommand, Result<int>>
{
    /// <summary>
    /// Handles the <see cref="CreateFlightCommand"/> to create a new flight.
    /// This involves validating associated entities (airplane, gates), generating a unique flight number,
    /// and setting the initial flight status to 'Scheduled'.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{Int32}"/> indicating the success or failure of the operation, with the ID of the created flight on success.</returns>
    public async Task<Result<int>> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        Airplane? airplane = await unitOfWork.Airplanes.GetByIdAsync(request.Dto.AirplaneId);
        if (airplane == null)
            return Result.Failure<int>("Airplane not found", ResultStatusCode.NotFound);

        Gate? departureGate = await unitOfWork.Gates.GetByIdAsync(request.Dto.DepartureGateId);
        if (departureGate == null)
            return Result.Failure<int>("Departure gate not found", ResultStatusCode.NotFound);

        Gate? arrivalGate = null;
        if (request.Dto.ArrivalGateId.HasValue)
        {
            arrivalGate = await unitOfWork.Gates.GetByIdAsync(request.Dto.ArrivalGateId.Value);
            if (arrivalGate == null)
                return Result.Failure<int>("Arrival gate not found", ResultStatusCode.NotFound);
        }


        var flight = mapper.Map<Flight>(request.Dto);
        flight.FlightNumber = await GenerateUniqueFlightNumberAsync();
        flight.Airplane = airplane;
        flight.DepartureGate = departureGate;
        flight.ArrivalGate = arrivalGate!;
        flight.FlightStatusId = (int)FlightStatusEnum.Scheduled;


        await unitOfWork.Flights.AddAsync(flight);
        await unitOfWork.CompleteAsync();
        return Result.Success(flight.Id,ResultStatusCode.Created);
    }

    /// <summary>
    /// Generates a unique flight number by repeatedly generating random flight numbers
    /// until one that does not already exist in the database is found.
    /// </summary>
    /// <returns>A <see cref="Task{String}"/> representing the asynchronous operation. The task result contains a unique flight number.</returns>
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

    /// <summary>
    /// Generates a random flight number string in the format "FLXXX", where XXX is a 3-digit number.
    /// </summary>
    /// <returns>A random flight number string.</returns>
    private string GenerateRandomFlightNumber()
    {
        var random = new Random();
        return $"FL{random.Next(100, 1000)}";
    }
}