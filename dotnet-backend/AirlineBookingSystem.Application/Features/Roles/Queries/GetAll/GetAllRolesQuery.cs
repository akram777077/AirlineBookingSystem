using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.GetAll;

/// <summary>
/// Represents a query to retrieve all roles.
/// </summary>
public record GetAllRolesQuery : IRequest<Result<IReadOnlyList<RoleDto>>>;

