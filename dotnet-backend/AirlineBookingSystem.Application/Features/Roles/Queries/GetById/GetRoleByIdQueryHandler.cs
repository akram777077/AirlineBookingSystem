using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Roles;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Roles.Queries.GetById;

/// <summary>
/// Handles the retrieval of a role by its unique identifier.
/// </summary>
public class GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
    /// <summary>
    /// Handles the <see cref="GetRoleByIdQuery"/> to retrieve a role by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{RoleDto}"/> indicating the success or failure of the operation, with the role DTO on success.</returns>
    public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.Roles.GetByIdAsync(request.Id);
        return role == null ? Result<RoleDto>.NotFound("Role not found.") : Result<RoleDto>.Success(mapper.Map<RoleDto>(role));
    }
}
