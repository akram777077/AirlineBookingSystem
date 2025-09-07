using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateUserStatus;

/// <summary>
/// Represents a command to activate a user account.
/// </summary>
/// <param name="Id">The unique identifier of the user to activate.</param>
public record ActivateUserCommand(
    int Id
    ) : IRequest<Result>;
