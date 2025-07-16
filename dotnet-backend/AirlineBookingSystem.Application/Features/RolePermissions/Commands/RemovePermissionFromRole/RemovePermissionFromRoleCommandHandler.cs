using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Enums;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;

public class RemovePermissionFromRoleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RemovePermissionFromRoleCommand, Result>
{
    public async Task<Result> Handle(RemovePermissionFromRoleCommand request, CancellationToken cancellationToken)
    {
        var rolePermission = await unitOfWork.RolePermissions.GetByRoleIdAndPermissionIdAsync(request.RoleId, request.PermissionId);
        if (rolePermission == null)
        {
            return Result.NotFound("Role permission not found.");
        }

        unitOfWork.RolePermissions.Delete(rolePermission);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.NoContent);
    }
}
