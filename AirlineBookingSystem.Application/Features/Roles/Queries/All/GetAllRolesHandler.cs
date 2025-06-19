using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.All;

public class GetAllRolesHandler (IRoleRepository roleRepository,IMapper mapper)
    : IRequestHandler<GetAllRolesQuery, IReadOnlyCollection<RoleDto>>
{
    public async Task<IReadOnlyCollection<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleRepository.GetAllAsync();
        return mapper.Map<IReadOnlyCollection<RoleDto>>(roles);
    }
}