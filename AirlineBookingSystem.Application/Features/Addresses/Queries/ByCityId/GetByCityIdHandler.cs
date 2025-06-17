using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.ByCityId;

public class GetByCityIdHandler(IAddressRepository addressRepository, IMapper mapper)
    : IRequestHandler<GetByCityIdQuery, AddressDto?>
{
    public async Task<AddressDto?> Handle(GetByCityIdQuery request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.GetByCityIdAsync(request.CityId);
        return mapper.Map<AddressDto>(address);
    }
}