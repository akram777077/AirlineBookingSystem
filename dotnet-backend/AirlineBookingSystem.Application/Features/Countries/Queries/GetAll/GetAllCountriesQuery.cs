using AirlineBookingSystem.Shared.DTOs.countries;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;

public record GetAllCountriesQuery : IRequest<IEnumerable<CountryDto>>;
