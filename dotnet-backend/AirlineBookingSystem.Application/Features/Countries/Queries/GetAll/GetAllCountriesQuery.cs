using AirlineBookingSystem.Shared.DTOs.countries;
using MediatR;
using AirlineBookingSystem.Shared.Results;

namespace AirlineBookingSystem.Application.Features.Countries.Queries.GetAll;

/// <summary>
/// Represents a query to retrieve all countries.
/// </summary>
public record GetAllCountriesQuery : IRequest<Result<List<CountryDto>>>;

