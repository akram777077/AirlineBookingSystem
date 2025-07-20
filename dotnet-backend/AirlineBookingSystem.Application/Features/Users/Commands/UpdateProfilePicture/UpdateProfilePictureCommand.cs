using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateProfilePicture;

public record UpdateProfilePictureCommand(int UserId, byte[] FileContent, string FileName) : IRequest<Result>;
