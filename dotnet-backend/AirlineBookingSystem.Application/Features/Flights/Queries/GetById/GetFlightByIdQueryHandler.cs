using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using AirlineBookingSystem.Application.Interfaces.Services;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetById;

public class GetFlightByIdQueryHandler (IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
    : IRequestHandler<GetFlightByIdQuery, Result<FlightDetailsDto>>
{
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