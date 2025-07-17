using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;

public class SearchAirplanesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SearchAirplanesQuery, PagedResult<List<AirplaneDto>>>
{
    public async Task<PagedResult<List<AirplaneDto>>> Handle(SearchAirplanesQuery request, CancellationToken cancellationToken)
    {
        var airplanesQuery = unitOfWork.Airplanes.GetAll();

        if (!string.IsNullOrWhiteSpace(request.Filter.Model))
        {
            airplanesQuery = airplanesQuery.Where(a => a.Model.Contains(request.Filter.Model));
        }

        if (!string.IsNullOrWhiteSpace(request.Filter.Manufacturer))
        {
            airplanesQuery = airplanesQuery.Where(a => a.Manufacturer.Contains(request.Filter.Manufacturer));
        }

        var airplaneDtosQuery = airplanesQuery.ProjectTo<AirplaneDto>(mapper.ConfigurationProvider);
        return await airplaneDtosQuery.ToPagedResult(request.Filter.PageNumber, request.Filter.PageSize);
    }
}
