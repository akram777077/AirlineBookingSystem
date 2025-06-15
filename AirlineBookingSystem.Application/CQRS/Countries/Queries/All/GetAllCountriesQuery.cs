using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Countries;
using MediatR;

namespace AirlineBookingSystem.Application.CQRS.Countries.Queries.All;

public class GetAllCountriesQuery() : IRequest<List<CountryDto>>;