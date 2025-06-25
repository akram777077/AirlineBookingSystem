using AirlineBookingSystem.Shared.DTOs.Addresses;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.ById;

public record GetAddressByIdQuery(int Id) : IRequest<AddressDto?>;