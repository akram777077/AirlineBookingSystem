using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.Search;

public record SearchAirportsQuery(AirportSearchFilter Filter) : IRequest<PagedResult<List<AirportSearchResultDto>>>;