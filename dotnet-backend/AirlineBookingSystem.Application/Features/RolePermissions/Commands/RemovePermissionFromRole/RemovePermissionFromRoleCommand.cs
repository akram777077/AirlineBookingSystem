using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;

public record RemovePermissionFromRoleCommand(int RoleId, int PermissionId) : IRequest<Result>;
