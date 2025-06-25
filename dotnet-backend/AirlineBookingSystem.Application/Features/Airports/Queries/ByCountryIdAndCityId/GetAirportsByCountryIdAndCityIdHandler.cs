using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Airports;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Queries.ByCountryIdAndCityId;

public class GetAirportsByCountryIdAndCityIdHandler (IAirportRepository repo, IMapper mapper)
    : IRequestHandler<GetAirportsByCountryIdAndCityIdQuery, IReadOnlyCollection<AirportDto>>
{
    public async Task<IReadOnlyCollection<AirportDto>> Handle(GetAirportsByCountryIdAndCityIdQuery request, CancellationToken cancellationToken)
    {
        var airports = await repo.GetByCountryIdAndCityIdAsync(request.CountryId, request.CityId);
        return mapper.Map<IReadOnlyCollection<AirportDto>>(airports);
    }
}