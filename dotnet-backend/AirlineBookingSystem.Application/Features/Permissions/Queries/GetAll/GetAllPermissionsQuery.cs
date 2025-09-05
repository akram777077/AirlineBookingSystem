using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Permissions.Queries.GetAll;

/// <summary>
/// Represents a query to retrieve all permissions.
/// </summary>
public record GetAllPermissionsQuery : IRequest<Result<IReadOnlyList<PermissionDto>>>;