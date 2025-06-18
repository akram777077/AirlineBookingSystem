using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.ById;

public class GetByIdHandler(IAddressRepository addressRepository, IMapper mapper) 
    : IRequestHandler<GetByIdQuery, AddressDto?>
{
    public async Task<AddressDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.GetByIdAsync(request.Id);
        return mapper.Map<AddressDto>(address);
    }
}