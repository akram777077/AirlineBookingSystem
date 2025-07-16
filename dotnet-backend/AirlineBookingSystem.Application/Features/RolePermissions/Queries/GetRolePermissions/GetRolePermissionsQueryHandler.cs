using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;

public class GetRolePermissionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetRolePermissionsQuery, Result<IReadOnlyList<PermissionDto>>>
{
    public async Task<Result<IReadOnlyList<PermissionDto>>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.Roles.GetByIdAsync(request.RoleId);
        if (role == null)
        {
            return Result<IReadOnlyList<PermissionDto>>.NotFound("Role not found.");
        }

        var permissions = await unitOfWork.RolePermissions.GetPermissionsByRoleIdAsync(request.RoleId);
        return Result<IReadOnlyList<PermissionDto>>.Success(mapper.Map<IReadOnlyList<PermissionDto>>(permissions));
    }
}
