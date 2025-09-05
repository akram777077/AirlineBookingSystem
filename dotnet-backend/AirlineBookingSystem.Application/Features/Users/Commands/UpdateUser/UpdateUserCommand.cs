using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateUser;

/// <summary>
/// Represents a command to update an existing user's details.
/// </summary>
/// <param name="Username">The updated username for the user.</param>
/// <param name="Email">The updated email address of the user.</param>
/// <param name="FirstName">The updated first name of the user.</param>
/// <param name="LastName">The updated last name of the user.</param>
/// <param name="MidName">The updated middle name of the user (optional).</param>
/// <param name="DateOfBirth">The updated date of birth of the user.</param>
/// <param name="GenderId">The updated ID of the gender of the user.</param>
/// <param name="Street">The updated street address of the user.</param>
/// <param name="CityId">The updated ID of the city where the user resides.</param>
/// <param name="ZipCode">The updated zip code of the user's address.</param>
/// <param name="RoleId">The updated ID of the role assigned to the user.</param>
public record UpdateUserCommand(
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
