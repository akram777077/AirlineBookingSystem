using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Auth.Queries.Login;

public class LoginUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginUserQuery, Result>
{
    public async Task<Result> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetUserWithPersonAsync(request.Username);
        if (user == null)
        {
            return Result.NotFound("User not found.");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return Result.Unauthorized("Invalid credentials.");
        }

        return Result.Success();
    }
}
