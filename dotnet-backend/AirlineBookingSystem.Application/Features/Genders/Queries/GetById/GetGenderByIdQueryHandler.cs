using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Genders;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Genders.Queries.GetById;

public class GetGenderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetGenderByIdQuery, Result<GenderDto>>
{
    public async Task<Result<GenderDto>> Handle(GetGenderByIdQuery request, CancellationToken cancellationToken)
    {
        var gender = await unitOfWork.Genders.GetByIdAsync(request.Id);
        return gender == null ? Result<GenderDto>.NotFound("Gender not found.") : Result<GenderDto>.Success(mapper.Map<GenderDto>(gender));
    }
}
