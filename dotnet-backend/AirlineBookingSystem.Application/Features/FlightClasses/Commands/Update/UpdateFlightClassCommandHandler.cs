using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Commands.Update;

/// <summary>
/// Handles the update of an existing flight class.
/// </summary>
public class UpdateFlightClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateFlightClassCommand, Result<int>>
{
    /// <summary>
    /// Handles the <see cref="UpdateFlightClassCommand"/> to update an existing flight class.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{Int32}"/> indicating the success or failure of the operation, with the ID of the updated flight class on success.</returns>
    public async Task<Result<int>> Handle(UpdateFlightClassCommand request, CancellationToken cancellationToken)
    {
        var flightClass = await unitOfWork.FlightClasses.GetByIdAsync(request.UpdateFlightClassDto.Id);
        if (flightClass == null)
                        return Result.Failure<int>("FlightClass not found", ResultStatusCode.NotFound);

        mapper.Map(request.UpdateFlightClassDto, flightClass);
        unitOfWork.FlightClasses.Update(flightClass);
        await unitOfWork.CompleteAsync();
        return Result.Success(flightClass.Id, ResultStatusCode.Success);
    }
    }