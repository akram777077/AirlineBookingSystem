using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.Search;

/// <summary>
/// Handles the search for airports based on a filter.
/// </summary>
public class SearchAirportsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchAirportsQuery, PagedResult<List<AirportSearchResultDto>>>
{
    /// <summary>
    /// Handles the <see cref="SearchAirportsQuery"/> to search for airports.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="PagedResult{List{AirportSearchResultDto}}"/> containing a paginated list of airport search result DTOs.</returns>
    public async Task<PagedResult<List<AirportSearchResultDto>>> Handle(SearchAirportsQuery request, CancellationToken cancellationToken)
    {
        var airportsQuery = unitOfWork.Airports.GetAll();
        var airportSearchResultDtosQuery = airportsQuery.ProjectTo<AirportSearchResultDto>(mapper.ConfigurationProvider);
        return await airportSearchResultDtosQuery.ToPagedResult(request.Filter.PageNumber, request.Filter.PageSize);
    }
}