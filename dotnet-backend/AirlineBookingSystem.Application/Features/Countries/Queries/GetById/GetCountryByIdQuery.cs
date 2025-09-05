using AirlineBookingSystem.Shared.DTOs.countries;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a country by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the country.</param>
public record GetCountryByIdQuery(int Id) : IRequest<Result<CountryDto>>;

