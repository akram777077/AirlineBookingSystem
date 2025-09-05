using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Filters;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Queries.Search;

/// <summary>
/// Represents a query to search for users based on a filter.
/// </summary>
/// <param name="Filter">The filter criteria for searching users.</param>
public record SearchUsersQuery(UserSearchFilter Filter) : IRequest<PagedResult<List<UserDto>>>;

