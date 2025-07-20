using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Queries.Search;

public class SearchUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<SearchUsersQuery, PagedResult<List<UserDto>>>
{
    public async Task<PagedResult<List<UserDto>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = unitOfWork.Users.SearchUsers(request.Filter);
        var userDtoQuery = usersQuery.ProjectTo<UserDto>(mapper.ConfigurationProvider);
        return await PagedResult<UserDto>.ToPagedList(userDtoQuery, request.Filter.PageNumber, request.Filter.PageSize);
    }
}
