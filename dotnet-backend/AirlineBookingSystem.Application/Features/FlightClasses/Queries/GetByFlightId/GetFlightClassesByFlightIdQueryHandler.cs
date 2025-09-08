using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;


namespace AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetByFlightId;

/// <summary>
/// Handles the retrieval of flight classes associated with a specific flight ID.
/// </summary>
public class GetFlightClassesByFlightIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetFlightClassesByFlightIdQuery, Result<IEnumerable<FlightClassDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetFlightClassesByFlightIdQuery"/> to retrieve flight classes by flight ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{IEnumerable{FlightClassDto}}"/> containing a list of flight class DTOs on success.</returns>
    public async Task<Result<IEnumerable<FlightClassDto>>> Handle(GetFlightClassesByFlightIdQuery request, CancellationToken cancellationToken)
    {
        var flightClasses = (await unitOfWork.FlightClasses.GetAllAsync())
            .Where(fc => fc.FlightId == request.FlightId)
            .ToList();

        if (!flightClasses.Any())
        {
                        return Result.NotFound<IEnumerable<FlightClassDto>>($"No flight classes found for FlightId: {request.FlightId}.");
        }

        var flightClassDtos = mapper.Map<IEnumerable<FlightClassDto>>(flightClasses);
        return Result.Success(flightClassDtos);
    }
    }
