using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(int UserId) : IRequest<Result<UserDto>>;
