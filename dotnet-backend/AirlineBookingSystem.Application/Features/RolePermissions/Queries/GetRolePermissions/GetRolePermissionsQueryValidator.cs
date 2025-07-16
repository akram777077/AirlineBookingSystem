using FluentValidation;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;

public class GetRolePermissionsQueryValidator : AbstractValidator<GetRolePermissionsQuery>
{
    public GetRolePermissionsQueryValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("Role ID must be greater than 0.");
    }
}
