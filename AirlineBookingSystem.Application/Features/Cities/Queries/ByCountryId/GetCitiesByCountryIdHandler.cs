using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.ByCountryId;

public class GetCitiesByCountryIdHandler (ICityRepository cityRepository, IMapper mapper)
    : IRequestHandler<GetCitiesByCountryIdQuery, IReadOnlyCollection<CityDto>>
{
    public async Task<IReadOnlyCollection<CityDto>> Handle(GetCitiesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var cities= cityRepository.GetByCountryIdAsync(request.CountryId);
        return mapper.Map<List<CityDto>>(await cities);
    }
}