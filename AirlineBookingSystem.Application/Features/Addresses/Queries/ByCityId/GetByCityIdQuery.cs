using AirlineBookingSystem.Application.Features.Addresses.Queries.ByCountryId;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.ByCityId;

public record GetByCityIdQuery(int CityId) : IRequest<AddressDto?>;