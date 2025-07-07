using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Query.Search;

public class SearchFlightsQueryHandler (IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchFlightsQuery, Result<IReadOnlyList<FlightSearchResultDto>>>
{
    public async Task<Result<IReadOnlyList<FlightSearchResultDto>>> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
    {
        var flights = await unitOfWork.Flights.GetFlightsWithDetailsAsync(request.Filter);
        var flightSearchResultDtos = mapper.Map<List<FlightSearchResultDto>>(flights);
        return Result<IReadOnlyList<FlightSearchResultDto>>.Success(flightSearchResultDtos);
        
    }
}