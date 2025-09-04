using AirlineBookingSystem.Shared.Results;
using MediatR;
using AirlineBookingSystem.Domain.Entities;

namespace AirlineBookingSystem.Application.Features.Auth.Queries.Login;

public record LoginUserQuery(string Username, string Password) : IRequest<Result<User>>;
