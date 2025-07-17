using AirlineBookingSystem.Shared.DTOs.countries;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetById;

public record GetCountryByIdQuery(int Id) : IRequest<Result<CountryDto>>;
