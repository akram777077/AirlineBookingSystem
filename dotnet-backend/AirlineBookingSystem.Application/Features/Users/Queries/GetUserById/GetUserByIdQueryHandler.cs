using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// Handles the retrieval of a user by their unique identifier.
/// </summary>
public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    /// <summary>
    /// Handles the <see cref="GetUserByIdQuery"/> to retrieve a user by their ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{UserDto}"/> indicating the success or failure of the operation, with the user DTO on success.</returns>
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            return Result<UserDto>.NotFound("User not found.");
        }

        var userDto = mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(userDto);
    }
}
