using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.DeleteProfilePicture;

public record DeleteProfilePictureCommand(int UserId) : IRequest<Result>;
