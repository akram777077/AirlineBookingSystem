using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Queries.Search;

public record SearchUsersQuery(UserSearchFilter Filter) : IRequest<PagedResult<List<UserDto>>>;
