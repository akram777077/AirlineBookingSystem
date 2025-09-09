
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AirlineBookingSystem.Application.Features.Users.Queries.Search;

/// <summary>
/// Handles the search for users based on a filter.
/// </summary>
public class SearchUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<SearchUsersQuery, PagedResult<List<UserDto>>>
{
    /// <summary>
    /// Handles the <see cref="SearchUsersQuery"/> to search for users.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="PagedResult{List{UserDto}}"/> containing a paginated list of user DTOs.</returns>
    public async Task<PagedResult<List<UserDto>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = unitOfWork.Users.SearchUsers(request.Filter);
        var totalCount = await usersQuery.CountAsync(cancellationToken);
        var users = await usersQuery.Skip((request.Filter.PageNumber - 1) * request.Filter.PageSize).Take(request.Filter.PageSize).ToListAsync(cancellationToken);
        var userDtos = mapper.Map<List<UserDto>>(users);
        return new PagedResult<List<UserDto>>(userDtos, request.Filter.PageNumber, request.Filter.PageSize, totalCount);
    }
}

