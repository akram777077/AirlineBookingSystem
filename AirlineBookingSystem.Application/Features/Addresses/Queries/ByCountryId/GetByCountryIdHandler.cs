using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.ByCountryId;

public class GetByCountryIdHandler(IAddressRepository addressRepository, IMapper mapper)
    : IRequestHandler<GetByCountryIdQuery, AddressDto?>
{
    public async Task<AddressDto?> Handle(GetByCountryIdQuery request, CancellationToken cancellationToken)
    {
        var address = await addressRepository.GetByCountryIdAsync(request.CountryId);
        return mapper.Map<AddressDto>(address);
    }
}