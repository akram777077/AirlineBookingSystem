using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.RolePermissions.Queries.GetRolePermissions;

/// <summary>
/// Represents a query to retrieve all permissions assigned to a specific role.
/// </summary>
/// <param name="RoleId">The unique identifier of the role.</param>
public record GetRolePermissionsQuery(int RoleId) : IRequest<Result<IReadOnlyList<PermissionDto>>>;

