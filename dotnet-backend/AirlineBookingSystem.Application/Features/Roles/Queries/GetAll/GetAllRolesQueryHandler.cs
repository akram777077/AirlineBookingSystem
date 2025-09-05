using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.GetAll;

/// <summary>
/// Handles the retrieval of all roles.
/// </summary>
public class GetAllRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllRolesQuery, Result<IReadOnlyList<RoleDto>>>
{
    /// <summary>
    /// Handles the <see cref="GetAllRolesQuery"/> to retrieve all roles.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{IReadOnlyList{RoleDto}}"/> containing a list of role DTOs on success.</returns>
    public async Task<Result<IReadOnlyList<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await unitOfWork.Roles.GetAllAsync();
        return Result<IReadOnlyList<RoleDto>>.Success(mapper.Map<IReadOnlyList<RoleDto>>(roles));
    }
}
