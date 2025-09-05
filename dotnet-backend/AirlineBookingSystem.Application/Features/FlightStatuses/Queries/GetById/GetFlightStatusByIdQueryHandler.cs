using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;

/// <summary>
/// Handles the retrieval of a flight status by its unique identifier.
/// </summary>
public class GetFlightStatusByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetFlightStatusByIdQuery, Result<FlightStatusDto>>
{
    /// <summary>
    /// Handles the <see cref="GetFlightStatusByIdQuery"/> to retrieve a flight status by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{FlightStatusDto}"/> indicating the success or failure of the operation, with the flight status DTO on success.</returns>
    public async Task<Result<FlightStatusDto>> Handle(GetFlightStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var flightStatus = await unitOfWork.FlightStatuses.GetByIdAsync(request.Id);
        return flightStatus == null ? Result<FlightStatusDto>.NotFound("Flight status not found.") : Result<FlightStatusDto>.Success(mapper.Map<FlightStatusDto>(flightStatus));
    }
}
