using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.ByCountryId;

public class GetCityByCountryIdHandler (ICityRepository cityRepository, IMapper mapper)
    : IRequestHandler<GetCityByCountryIdQuery, IReadOnlyCollection<CityDto>>
{
    public async Task<IReadOnlyCollection<CityDto>> Handle(GetCityByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var cities= cityRepository.GetByCountryIdAsync(request.CountryId);
        return mapper.Map<List<CityDto>>(await cities);
    }
}