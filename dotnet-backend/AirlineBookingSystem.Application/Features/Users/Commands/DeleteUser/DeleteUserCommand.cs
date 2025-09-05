using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.DeleteUser;

/// <summary>
/// Represents a command to delete a user by their unique identifier.
/// </summary>
/// <param name="UserId">The unique identifier of the user to delete.</param>
public record DeleteUserCommand(int UserId) : IRequest<Result>;
