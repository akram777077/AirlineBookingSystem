using AirlineBookingSystem.Shared.DTOs.Addresses;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.ByCountryId;

public record GetByCountryIdQuery(int CountryId) : IRequest<AddressDto?>;