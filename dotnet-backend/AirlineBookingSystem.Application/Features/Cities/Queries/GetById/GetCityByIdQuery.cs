using AirlineBookingSystem.Shared.DTOs.Cities;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Cities.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a city by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the city.</param>
public record GetCityByIdQuery(int Id) : IRequest<Result<CityDto>>;
