using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.GetById;

/// <summary>
/// Handles the retrieval of an airport by its unique identifier.
/// </summary>
public class GetAirportByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAirportByIdQuery, Result<AirportDto>>
{
    /// <summary>
    /// Handles the <see cref="GetAirportByIdQuery"/> to retrieve an airport by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{AirportDto}"/> indicating the success or failure of the operation, with the airport's DTO on success.</returns>
    public async Task<Result<AirportDto>> Handle(GetAirportByIdQuery request, CancellationToken cancellationToken)
    {
        var airport = await unitOfWork.Airports.GetByIdAsync(request.Id);
        return airport == null ? Result.NotFound<AirportDto>("Airport not found.") : Result.Success(mapper.Map<AirportDto>(airport));
    }
}