using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Auth.Commands.Register;

/// <summary>
/// Handles the registration of a new user.
/// </summary>
public class RegisterUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="RegisterUserCommand"/> to register a new user.
    /// This involves creating a new Address, Person, and User entity, and saving them to the database.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the registration.</returns>
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var gender = await unitOfWork.Genders.GetByIdAsync(request.GenderId);
        if (gender == null)
        {
            return Result.NotFound("Gender not found.");
        }

        var role = await unitOfWork.Roles.GetByIdAsync((int)RoleEnum.Customer);
        if (role == null) 
        {
            return Result.NotFound("Default role not found.");
        }

        var city = await unitOfWork.Cities.GetByIdAsync(request.CityId);
        if (city == null)
        {
            return Result.NotFound("City not found.");
        }

        var address = new Address
        {
            Street = request.Street,
            CityId = request.CityId,
            City = city,
            ZipCode = request.ZipCode
        };

        var person = new Person
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MidName = request.MidName,
            DateOfBirth = request.DateOfBirth,
            GenderId = request.GenderId,
            Gender = gender,
            Email = request.Email,
            Address = address
        };

        var user = new User
        {
            Username = request.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Person = person,
            RoleId = (int)RoleEnum.Customer,
            Role = role
        };

        await unitOfWork.Users.AddAsync(user);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.Created);
    }
}
