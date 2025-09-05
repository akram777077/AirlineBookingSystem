using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Permissions.Queries.GetById;

/// <summary>
/// Handles the retrieval of a permission by its unique identifier.
/// </summary>
public class GetPermissionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetPermissionByIdQuery, Result<PermissionDto>>
{
    /// <summary>
    /// Handles the <see cref="GetPermissionByIdQuery"/> to retrieve a permission by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{PermissionDto}"/> indicating the success or failure of the operation, with the permission DTO on success.</returns>
    public async Task<Result<PermissionDto>> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var permission = await unitOfWork.Permissions.GetByIdAsync(request.Id);
        return permission == null ? Result<PermissionDto>.NotFound("Permission not found.") : Result<PermissionDto>.Success(mapper.Map<PermissionDto>(permission));
    }
}
