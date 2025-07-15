using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Query.Search;

public class SearchAirportsQuery(AirportSearchFilter filter) : IRequest<PagedResult<List<AirportSearchResultDto>>>
{
    public AirportSearchFilter Filter => filter;
}