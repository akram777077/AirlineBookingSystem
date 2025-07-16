using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetAll;

public class GetAllGendersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllGendersQuery, Result<IEnumerable<GenderDto>>>
{
    public async Task<Result<IEnumerable<GenderDto>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
    {
        var genders = await unitOfWork.Genders.GetAllAsync();
        return Result<IEnumerable<GenderDto>>.Success(mapper.Map<IEnumerable<GenderDto>>(genders));
    }
}
