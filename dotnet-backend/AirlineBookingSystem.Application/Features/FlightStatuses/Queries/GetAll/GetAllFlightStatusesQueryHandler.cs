using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;

/// <summary>
/// Handles the retrieval of all flight statuses.
/// </summary>
public class GetAllFlightStatusesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllFlightStatusesQuery, Result<IEnumerable<FlightStatusDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetAllFlightStatusesQuery"/> to retrieve all flight statuses.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{IEnumerable{FlightStatusDto}}"/> containing a list of flight status DTOs on success.</returns>
    public async Task<Result<IEnumerable<FlightStatusDto>>> Handle(GetAllFlightStatusesQuery request, CancellationToken cancellationToken)
    {
        var flightStatuses = await unitOfWork.FlightStatuses.GetAllAsync();
        return Result<IEnumerable<FlightStatusDto>>.Success(mapper.Map<IEnumerable<FlightStatusDto>>(flightStatuses));
    }
}
