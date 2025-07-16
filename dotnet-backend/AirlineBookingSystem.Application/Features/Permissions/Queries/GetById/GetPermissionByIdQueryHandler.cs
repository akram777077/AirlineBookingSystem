using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Permissions.Queries.GetById;

public class GetPermissionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetPermissionByIdQuery, Result<PermissionDto>>
{
    public async Task<Result<PermissionDto>> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var permission = await unitOfWork.Permissions.GetByIdAsync(request.Id);
        return permission == null ? Result<PermissionDto>.NotFound("Permission not found.") : Result<PermissionDto>.Success(mapper.Map<PermissionDto>(permission));
    }
}
