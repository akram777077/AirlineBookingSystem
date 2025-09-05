using AirlineBookingSystem.Shared.Results;
using MediatR;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Features.Auth.Queries.Login;

/// <summary>
/// Represents a query to log in a user.
/// </summary>
/// <param name="Username">The username of the user attempting to log in.</param>
/// <param name="Password">The password of the user attempting to log in.</param>
public record LoginUserQuery(string Username, string Password) : IRequest<Result<User>>;

