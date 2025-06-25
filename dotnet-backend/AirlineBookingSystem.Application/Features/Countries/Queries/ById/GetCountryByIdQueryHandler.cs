using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.ById;

public class GetCountryByIdQueryHandler(ICountryRepository countryRepository, IMapper mapper)
    : IRequestHandler<GetCountryByIdQuery, CountryDto?>
{
    public async Task<CountryDto?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetByIdAsync(request.Id);
        return country is null ? null : mapper.Map<CountryDto>(country);
    }
}