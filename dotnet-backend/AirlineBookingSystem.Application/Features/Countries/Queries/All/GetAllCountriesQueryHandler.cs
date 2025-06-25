using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.All;

public class GetAllCountriesQueryHandler(ICountryRepository countryRepository, IMapper mapper)
    : IRequestHandler<GetAllCountriesQuery, IReadOnlyCollection<CountryDto>>
{
    private readonly ICountryRepository _countryRepository = countryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyCollection<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _countryRepository.GetAllAsync();
        return _mapper.Map<List<CountryDto>>(countries);
    }
}