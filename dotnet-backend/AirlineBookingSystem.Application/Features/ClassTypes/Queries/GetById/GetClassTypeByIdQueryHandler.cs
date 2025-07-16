using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.ClassTypes;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.ClassTypes.Queries.GetById;

public class GetClassTypeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetClassTypeByIdQuery, Result<ClassTypeDto>>
{
    public async Task<Result<ClassTypeDto>> Handle(GetClassTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var classType = await unitOfWork.ClassTypes.GetByIdAsync(request.Id);
        return classType == null ? Result<ClassTypeDto>.NotFound("Class type not found.") : Result<ClassTypeDto>.Success(mapper.Map<ClassTypeDto>(classType));
    }
}
