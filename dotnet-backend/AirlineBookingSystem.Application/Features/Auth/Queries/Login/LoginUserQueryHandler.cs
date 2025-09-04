using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Features.Auth.Queries.Login;

public class LoginUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginUserQuery, Result<User>>
{
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
