using AirlineBookingSystem.Shared.DTOs.Addresses;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.ById;

public record GetByIdQuery(int Id) : IRequest<AddressDto?>;