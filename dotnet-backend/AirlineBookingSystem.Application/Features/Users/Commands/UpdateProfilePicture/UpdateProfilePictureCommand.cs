using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateProfilePicture;

/// <summary>
/// Represents a command to update a user's profile picture.
/// </summary>
/// <param name="UserId">The unique identifier of the user whose profile picture is to be updated.</param>
/// <param name="FileContent">The byte array representing the content of the new profile picture file.</param>
/// <param name="FileName">The original file name of the profile picture.</param>
public record UpdateProfilePictureCommand(int UserId, byte[] FileContent, string FileName) : IRequest<Result>;

