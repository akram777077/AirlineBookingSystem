using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.Search;

public record SearchCitiesQuery(CitySearchFilter Filter) : IRequest<PagedResult<List<CityDto>>>;
