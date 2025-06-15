using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.CQRS.Countries.Queries.All;

public class GetAllCountriesQueryHandler(ICountryRepository countryRepository, IMapper mapper)
    : IRequestHandler<GetAllCountriesQuery, List<CountryDto>>
{
    private readonly ICountryRepository _countryRepository = countryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _countryRepository.GetAllAsync();
        return _mapper.Map<List<CountryDto>>(countries);
    }
}