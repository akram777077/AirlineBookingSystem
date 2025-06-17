using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Addresses;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Addresses.Queries.List;

public class GetListHandler(IAddressRepository addressRepository, IMapper mapper)
    : IRequestHandler<GetListQuery, IReadOnlyCollection<AddressDto>>
{
    public async Task<IReadOnlyCollection<AddressDto>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var addresses = await addressRepository.GetAllAsync();
        return mapper.Map<IReadOnlyCollection<AddressDto>>(addresses);
    }
}