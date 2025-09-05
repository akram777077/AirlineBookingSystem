using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.CreateUser;

/// <summary>
/// Represents a command to create a new user in the system.
/// </summary>
/// <param name="Username">The desired username for the new user.</param>
/// <param name="Password">The password for the new user.</param>
/// <param name="Email">The email address of the new user.</param>
/// <param name="FirstName">The first name of the new user.</param>
/// <param name="LastName">The last name of the new user.</param>
/// <param name="MidName">The middle name of the new user (optional).</param>
/// <param name="DateOfBirth">The date of birth of the new user.</param>
/// <param name="GenderId">The ID of the gender of the new user.</param>
/// <param name="Street">The street address of the new user.</param>
/// <param name="CityId">The ID of the city where the new user resides.</param>
/// <param name="ZipCode">The zip code of the new user's address.</param>
/// <param name="RoleId">The ID of the role to assign to the new user.</param>
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
