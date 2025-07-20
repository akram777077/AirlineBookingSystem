using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Auth.Commands.Register;

public record RegisterUserCommand(
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
    string ZipCode
    ) : IRequest<Result>;
