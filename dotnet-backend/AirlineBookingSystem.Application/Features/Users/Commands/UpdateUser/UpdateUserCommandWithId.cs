using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommandWithId(
    int UserId,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string? MidName,
    DateTime DateOfBirth,
    int GenderId,
    string Street,
    int CityId,
    string ZipCode,
    int RoleId
    ) : IRequest<Result>;
