using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.GetAll;

public record GetAllRolesQuery : IRequest<Result<IReadOnlyList<RoleDto>>>;
