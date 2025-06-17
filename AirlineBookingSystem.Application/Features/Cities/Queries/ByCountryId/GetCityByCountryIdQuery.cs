using AirlineBookingSystem.Shared.DTOs.Countries;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.ByCountryId;

public record GetCityByCountryIdQuery(int CountryId) : IRequest<IReadOnlyCollection<CityDto>>;