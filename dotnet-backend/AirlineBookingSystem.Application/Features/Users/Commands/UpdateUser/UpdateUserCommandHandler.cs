using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.UpdateUser;

/// <summary>
/// Handles the update of an existing user's details.
/// </summary>
public class UpdateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommandWithId, Result>
{
    /// <summary>
    /// Handles the <see cref="UpdateUserCommandWithId"/> to update an existing user's details.
    /// This involves validating associated entities (gender, role, city) and updating the user's Person, Address, and User properties.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the user update.</returns>
    public async Task<Result> Handle(UpdateUserCommandWithId request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            return Result.NotFound("User not found.");
        }

        var gender = await unitOfWork.Genders.GetByIdAsync(request.GenderId);
        if (gender == null)
        {
            return Result.NotFound("Gender not found.");
        }

        var role = await unitOfWork.Roles.GetByIdAsync(request.RoleId);
        if (role == null)
        {
            return Result.NotFound("Role not found.");
        }

        var city = await unitOfWork.Cities.GetByIdAsync(request.CityId);
        if (city == null)
        {
            return Result.NotFound("City not found.");
        }

        // Update Person properties
        user.Person.FirstName = request.FirstName;
        user.Person.LastName = request.LastName;
        user.Person.MidName = request.MidName;
        user.Person.DateOfBirth = request.DateOfBirth;
        user.Person.GenderId = request.GenderId;
        user.Person.Gender = gender;
        user.Person.Email = request.Email;

        // Update Address properties
        user.Person.Address.Street = request.Street;
        user.Person.Address.CityId = request.CityId;
        user.Person.Address.City = city;
        user.Person.Address.ZipCode = request.ZipCode;

        // Update User properties
        user.Username = request.Username;
        user.RoleId = request.RoleId;
        user.Role = role;
        user.UpdatedAt = DateTime.UtcNow;

        unitOfWork.Users.Update(user);
        await unitOfWork.CompleteAsync();

        return Result.NoContent();
    }
}
