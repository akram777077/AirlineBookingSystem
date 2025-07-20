using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Auth.Queries.Login;

public record LoginUserQuery(string Username, string Password) : IRequest<Result>;
