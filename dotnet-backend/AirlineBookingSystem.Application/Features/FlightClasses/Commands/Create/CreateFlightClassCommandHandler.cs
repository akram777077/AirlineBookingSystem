using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.FlightClass;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Create;

/// <summary>
/// Handles the creation of a new flight class.
/// </summary>
public class CreateFlightClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateFlightClassCommand, Result<int>>
{
    /// <summary>
    /// Handles the <see cref="CreateFlightClassCommand"/> to create a new flight class.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{Int32}"/> indicating the success or failure of the operation, with the ID of the created flight class on success.</returns>
    public async Task<Result<int>> Handle(CreateFlightClassCommand request, CancellationToken cancellationToken)
    {
        var flight = await unitOfWork.Flights.GetByIdAsync(request.CreateFlightClassDto.FlightId);
        if (flight == null)
            return Result<int>.Failure("Flight not found", ResultStatusCode.NotFound);

        var classType = await unitOfWork.ClassTypes.GetByIdAsync(request.CreateFlightClassDto.ClassId);
        if (classType == null)
            return Result<int>.Failure("ClassType not found", ResultStatusCode.NotFound);

        var flightClass = mapper.Map<FlightClass>(request.CreateFlightClassDto);
        await unitOfWork.FlightClasses.AddAsync(flightClass);
        await unitOfWork.CompleteAsync();
        return Result<int>.Success(flightClass.Id,ResultStatusCode.Created);
    }
}
