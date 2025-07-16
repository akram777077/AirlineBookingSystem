using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.GetById;

public record GetRoleByIdQuery(int Id) : IRequest<Result<RoleDto>>;
