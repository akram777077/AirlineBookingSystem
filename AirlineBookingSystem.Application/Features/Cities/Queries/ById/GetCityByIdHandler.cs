using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.ById;

public class GetCityByIdHandler (ICityRepository cityRepository, IMapper mapper)
    : IRequestHandler<GetCityByIdQuery, CityDto?>
{
    public async Task<CityDto?> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await cityRepository.GetByIdAsync(request.Id);
        return mapper.Map<CityDto>(city);
    }
}