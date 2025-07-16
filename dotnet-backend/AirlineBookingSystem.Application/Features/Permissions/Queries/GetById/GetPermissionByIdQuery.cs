using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Permissions.Queries.GetById;

public class GetPermissionByIdQuery(int id) : IRequest<Result<PermissionDto>>
{
    public int Id => id;
}
