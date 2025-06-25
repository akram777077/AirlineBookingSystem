using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.ById;

public class GetAddressByIdHandler(IAddressRepository addressRepository, IMapper mapper) 
    : IRequestHandler<GetAddressByIdQuery, AddressDto?>
{
    public async Task<AddressDto?> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.GetByIdAsync(request.Id);
        return mapper.Map<AddressDto>(address);
    }
}