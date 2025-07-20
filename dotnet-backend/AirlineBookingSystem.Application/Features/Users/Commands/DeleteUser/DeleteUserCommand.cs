using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(int UserId) : IRequest<Result>;
