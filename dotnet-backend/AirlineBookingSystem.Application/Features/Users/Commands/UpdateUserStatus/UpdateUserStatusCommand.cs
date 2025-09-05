using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateUserStatus;

/// <summary>
/// Represents a command to update a user's active status.
/// </summary>
/// <param name="UserId">The unique identifier of the user whose status is to be updated.</param>
/// <param name="IsActive">The new active status for the user (true for active, false for inactive).</param>
public record UpdateUserStatusCommand(int UserId, bool IsActive) : IRequest<Result>;
