using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetAll;

public class GetAllClassTypesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllClassTypesQuery, Result<IEnumerable<ClassTypeDto>>>
{
    public async Task<Result<IEnumerable<ClassTypeDto>>> Handle(GetAllClassTypesQuery request, CancellationToken cancellationToken)
    {
        var classTypes = await unitOfWork.ClassTypes.GetAllAsync();
        return Result<IEnumerable<ClassTypeDto>>.Success(mapper.Map<IEnumerable<ClassTypeDto>>(classTypes));
    }
}
