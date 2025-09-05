using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using AirlineBookingSystem.Application.Interfaces.Services;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetById;

/// <summary>
/// Handles the retrieval of flight details by its unique identifier, utilizing caching.
/// </summary>
public class GetFlightByIdQueryHandler (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
    : IRequestHandler<GetFlightByIdQuery, Result<FlightDetailsDto>>
{
    /// <summary>
    /// Handles the <see cref="GetFlightByIdQuery"/> to retrieve flight details by ID.
    /// It first attempts to retrieve the flight from the cache. If not found, it fetches from the database and caches the result.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{FlightDetailsDto}"/> indicating the success or failure of the operation, with the flight details DTO on success.</returns>
    public async Task<Result<FlightDetailsDto>> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"Flight:{request.Id}";
        var flight = await cacheService.GetOrCreateAsync(cacheKey, async () =>
        {
            return await unitOfWork.Flights.GetByIdAsync(request.Id);
        }, TimeSpan.FromMinutes(5)); // Cache for 5 minutes

        if (flight == null)
        {
            return Result<FlightDetailsDto>.Failure("Flight not found", ResultStatusCode.NotFound);
        }

        var flightDetailsDto = mapper.Map<FlightDetailsDto>(flight);
        return Result<FlightDetailsDto>.Success(flightDetailsDto);
    }
}