using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.Search;

public class SearchFlightsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<SearchFlightsQuery, PagedResult<List<FlightSearchResultDto>>>
{
    public async Task<PagedResult<List<FlightSearchResultDto>>> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
    {
        var flightsQuery = unitOfWork.Flights.GetFlightsWithDetails(request.Filter);
        var flightSearchResultDtosQuery = flightsQuery.ProjectTo<FlightSearchResultDto>(mapper.ConfigurationProvider);
        return await flightSearchResultDtosQuery.ToPagedResult(request.Filter.PageNumber, request.Filter.PageSize);
        
    }
}