
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.Search;

/// <summary>
/// Handles the search for cities based on a filter.
/// </summary>
public class SearchCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchCitiesQuery, PagedResult<List<CityDto>>>
{
    /// <summary>
    /// Handles the <see cref="SearchCitiesQuery"/> to search for cities.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="PagedResult{List{CityDto}}"/> containing a paginated list of city DTOs.</returns>
    public async Task<PagedResult<List<CityDto>>> Handle(SearchCitiesQuery request, CancellationToken cancellationToken)
    {
        var query = unitOfWork.Cities.GetAll();

        if (request.Filter.CountryId.HasValue)
        {
            query = query.Where(c => c.CountryId == request.Filter.CountryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Filter.Name))
        {
            query = query.Where(c => c.Name.Contains(request.Filter.Name));
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var cities = await query.Skip((request.Filter.PageNumber - 1) * request.Filter.PageSize).Take(request.Filter.PageSize).ToListAsync(cancellationToken);
        var cityDtos = mapper.Map<List<CityDto>>(cities);

        return new PagedResult<List<CityDto>>(cityDtos, request.Filter.PageNumber, request.Filter.PageSize, totalCount);
    }
}

