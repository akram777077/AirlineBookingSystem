using AirlineBookingSystem.Shared.DTOs.Addresses;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.List;

public record GetListQuery() : IRequest<IReadOnlyCollection<AddressDto>>;