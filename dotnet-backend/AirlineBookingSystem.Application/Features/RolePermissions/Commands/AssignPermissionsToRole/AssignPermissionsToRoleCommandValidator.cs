using FluentValidation;
using System.Linq;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.AssignPermissionsToRole;

public class AssignPermissionsToRoleCommandValidator : AbstractValidator<AssignPermissionsToRoleCommand>
{
    public AssignPermissionsToRoleCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("Role ID must be greater than 0.");

        RuleFor(x => x.PermissionIds)
            .NotNull().WithMessage("Permission IDs cannot be null.");

        RuleForEach(x => x.PermissionIds)
            .GreaterThan(0).WithMessage("All Permission IDs must be greater than 0.");
    }
}
