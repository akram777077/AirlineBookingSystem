using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.Shared.Results;
using AirlineBookingSystem.Shared.Results.Error;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Queries.GetById;

public class GetGateByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetGateByIdQuery, Result<GateDto>>
{
    public async Task<Result<GateDto>> Handle(GetGateByIdQuery request, CancellationToken cancellationToken)
    {
        var gate = await unitOfWork.Gates.GetByIdAsync(request.Id);
        if (gate == null)
        {
            return Result<GateDto>.Failure("Gate NotFound");
        }
        return Result<GateDto>.Success(mapper.Map<GateDto>(gate));
    }
}
