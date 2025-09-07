using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateUserStatus;

/// <summary>
/// Represents a command to deactivate a user account.
/// </summary>
/// <param name="Id">The unique identifier of the user to deactivate.</param>
public record DeactivateUserCommand(
    int Id
    ) : IRequest<Result>;
