using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetById;

/// <summary>
/// Handles the retrieval of a flight class by its unique identifier.
/// </summary>
public class GetFlightClassByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetFlightClassByIdQuery, Result<FlightClassDto>>
{
    /// <summary>
    /// Handles the <see cref="GetFlightClassByIdQuery"/> to retrieve a flight class by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{FlightClassDto}"/> indicating the success or failure of the operation, with the flight class DTO on success.</returns>
    public async Task<Result<FlightClassDto>> Handle(GetFlightClassByIdQuery request, CancellationToken cancellationToken)
    {
        var flightClass = await unitOfWork.FlightClasses.GetByIdAsync(request.Id);

        if (flightClass == null)
        {
                        return Result.NotFound<FlightClassDto>("FlightClass not found.");
        }

        var flightClassDto = mapper.Map<FlightClassDto>(flightClass);
        return Result.Success(flightClassDto);
    }
    }
