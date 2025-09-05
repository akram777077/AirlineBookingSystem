using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Permissions.Queries.GetById;

/// <summary>
/// Represents a query to retrieve a permission by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the permission.</param>
public record GetPermissionByIdQuery(int Id) : IRequest<Result<PermissionDto>>;