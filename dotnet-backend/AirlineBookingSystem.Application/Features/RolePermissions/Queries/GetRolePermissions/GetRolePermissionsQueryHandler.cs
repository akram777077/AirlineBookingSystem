using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;

/// <summary>
/// Handles the retrieval of permissions assigned to a specific role.
/// </summary>
public class GetRolePermissionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetRolePermissionsQuery, Result<IReadOnlyList<PermissionDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetRolePermissionsQuery"/> to retrieve permissions assigned to a role.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{IReadOnlyList{PermissionDto}}"/> indicating the success or failure of the operation, with a list of permission DTOs on success.</returns>
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
