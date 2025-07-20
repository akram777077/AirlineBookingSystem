using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Users;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
        {
            return Result<UserDto>.NotFound("User not found.");
        }

        var userDto = mapper.Map<UserDto>(user);
        return Result<UserDto>.Success(userDto);
    }
}
