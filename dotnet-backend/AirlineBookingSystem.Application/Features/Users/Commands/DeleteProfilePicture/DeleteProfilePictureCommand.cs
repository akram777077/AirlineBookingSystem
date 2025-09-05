using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.DeleteProfilePicture;

/// <summary>
/// Represents a command to delete a user's profile picture.
/// </summary>
/// <param name="UserId">The unique identifier of the user whose profile picture is to be deleted.</param>
public record DeleteProfilePictureCommand(int UserId) : IRequest<Result>;
