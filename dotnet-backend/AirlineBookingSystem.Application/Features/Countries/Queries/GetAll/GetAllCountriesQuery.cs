using AirlineBookingSystem.Shared.DTOs.countries;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;

public record GetAllCountriesQuery : IRequest<Result<List<CountryDto>>>;
