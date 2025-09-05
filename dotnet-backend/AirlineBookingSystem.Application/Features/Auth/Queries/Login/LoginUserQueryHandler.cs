using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Features.Auth.Queries.Login;

/// <summary>
/// Handles the login query for user authentication.
/// </summary>
public class LoginUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginUserQuery, Result<User>>
{
    /// <summary>
    /// Handles the <see cref="LoginUserQuery"/> to authenticate a user.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{User}"/> indicating the success or failure of the authentication, with the authenticated user on success.</returns>
    public async Task<Result<User>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetUserWithPersonAsync(request.Username);
        if (user == null)
        {
            return Result<User>.NotFound("User not found.");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Result<User>.Unauthorized("Invalid credentials.");
        }

        return Result<User>.Success(user);
    }
}
