using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
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
            Password = request.Password, // In a real application, this should be hashed
            Person = person,
            RoleId = request.RoleId,
            Role = role
        };

        await unitOfWork.Users.AddAsync(user);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.Created);
    }
}
