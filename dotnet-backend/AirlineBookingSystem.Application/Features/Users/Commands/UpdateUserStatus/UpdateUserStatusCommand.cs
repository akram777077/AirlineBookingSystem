using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateUserStatus;

public record UpdateUserStatusCommand(int UserId, bool IsActive) : IRequest<Result>;
