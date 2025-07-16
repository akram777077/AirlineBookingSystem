using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.Search;

public class SearchCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchCitiesQuery, PagedResult<List<CityDto>>>
{
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

        var pagedResult = await PagedResult<CityDto>.ToPagedList(
            query.Select(c => mapper.Map<CityDto>(c)),
            request.Filter.PageNumber,
            request.Filter.PageSize);

        return pagedResult;
    }
}
