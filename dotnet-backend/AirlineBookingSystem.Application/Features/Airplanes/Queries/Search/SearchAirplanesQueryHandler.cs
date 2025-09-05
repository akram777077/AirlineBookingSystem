using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.airplanes;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Application.Features.Airplanes.Queries.Search;

/// <summary>
/// Represents a query handler for searching airplanes.
/// </summary>
public class SearchAirplanesQueryHandler: IRequestHandler<SearchAirplanesQuery, IEnumerable<AirplaneDto>>
{
    private readonly IAirplaneRepository _airplaneRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchAirplanesQueryHandler"/> class.
    /// </summary>
    /// <param name="airplaneRepository">The airplane repository.</param>
    /// <param name="mapper">The mapper.</param>
    public SearchAirplanesQueryHandler(IAirplaneRepository airplaneRepository, IMapper mapper)
    {
        _airplaneRepository = airplaneRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the search airplanes query.
    /// </summary>
    /// <param name="request">The search airplanes query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A collection of airplane DTOs.</returns>
    public async Task<IEnumerable<AirplaneDto>> Handle(SearchAirplanesQuery request, CancellationToken cancellationToken)
    {
        var airplanes =  _airplaneRepository.GetAll().AsNoTracking();
        
        if (!string.IsNullOrEmpty(request.Model))
            airplanes = airplanes.Where(a => a.Model.Contains(request.Model));
        
        if (!string.IsNullOrEmpty(request.Manufacturer))
            airplanes = airplanes.Where(a => a.Manufacturer.Contains(request.Manufacturer));
        
        if (request.MinCapacity.HasValue)
            airplanes = airplanes.Where(a => a.Capacity >= request.MinCapacity.Value);
        
        if (request.MaxCapacity.HasValue)
            airplanes = airplanes.Where(a => a.Capacity <= request.MaxCapacity.Value);
        
        if (!string.IsNullOrEmpty(request.Code))
            airplanes = airplanes.Where(a => a.Code.Contains(request.Code));
        
        return _mapper.Map<IEnumerable<AirplaneDto>>(await airplanes.ToListAsync(cancellationToken));
    }
}