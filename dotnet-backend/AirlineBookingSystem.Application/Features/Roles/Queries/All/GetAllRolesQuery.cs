using AirlineBookingSystem.Shared.DTOs.Roles;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.All;

public record GetAllRolesQuery() : IRequest<IReadOnlyCollection<RoleDto>>;