using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a role by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the role.</param>
public record GetRoleByIdQuery(int Id) : IRequest<Result<RoleDto>>;

