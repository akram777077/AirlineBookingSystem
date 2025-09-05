using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Queries.GetUserById;

using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Queries.GetUserById;

/// <summary>
/// Represents a query to retrieve a user by their unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
public record GetUserByIdQuery(int Id) : IRequest<Result<UserDto>>;
