using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.GetById;

public class GetRoleByIdQuery(int id) : IRequest<Result<RoleDto>>
{
    public int Id => id;
}
