using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.Search;

/// <summary>
/// Handles the search for flights based on a filter.
/// </summary>
public class SearchFlightsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<SearchFlightsQuery, PagedResult<List<FlightSearchResultDto>>>
{
    /// <summary>
    /// Handles the <see cref="SearchFlightsQuery"/> to search for flights.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="PagedResult{List{FlightSearchResultDto}}"/> containing a paginated list of flight search result DTOs.</returns>
    public async Task<PagedResult<List<FlightSearchResultDto>>> Handle(SearchFlightsQuery request, CancellationToken cancellationToken)
    {
        var flightsQuery = unitOfWork.Flights.GetFlightsWithDetails(request.Filter);
        var flightSearchResultDtosQuery = flightsQuery.ProjectTo<FlightSearchResultDto>(mapper.ConfigurationProvider);
        return await flightSearchResultDtosQuery.ToPagedResult(request.Filter.PageNumber, request.Filter.PageSize);
        
    }
}