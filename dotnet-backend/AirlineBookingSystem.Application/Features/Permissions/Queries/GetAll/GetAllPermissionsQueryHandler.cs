using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Permissions;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Permissions.Queries.GetAll;

/// <summary>
/// Handles the retrieval of all permissions.
/// </summary>
public class GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllPermissionsQuery, Result<IReadOnlyList<PermissionDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetAllPermissionsQuery"/> to retrieve all permissions.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{IReadOnlyList{PermissionDto}}"/> containing a list of permission DTOs on success.</returns>
    public async Task<Result<IReadOnlyList<PermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await unitOfWork.Permissions.GetAllAsync();
        return Result<IReadOnlyList<PermissionDto>>.Success(mapper.Map<IReadOnlyList<PermissionDto>>(permissions));
    }
}
