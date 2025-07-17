using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.Search;

public class SearchAirportsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchAirportsQuery, PagedResult<List<AirportSearchResultDto>>>
{
    public async Task<PagedResult<List<AirportSearchResultDto>>> Handle(SearchAirportsQuery request, CancellationToken cancellationToken)
    {
        var airportsQuery = unitOfWork.Airports.GetAll();
        var airportSearchResultDtosQuery = airportsQuery.ProjectTo<AirportSearchResultDto>(mapper.ConfigurationProvider);
        return await airportSearchResultDtosQuery.ToPagedResult(request.Filter.PageNumber, request.Filter.PageSize);
    }
}