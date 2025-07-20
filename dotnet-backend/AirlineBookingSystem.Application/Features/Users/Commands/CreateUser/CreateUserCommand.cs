using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string Password,
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
