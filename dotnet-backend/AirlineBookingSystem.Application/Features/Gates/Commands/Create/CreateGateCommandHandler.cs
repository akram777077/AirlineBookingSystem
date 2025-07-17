using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Create;

public class CreateGateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateGateCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateGateCommand request, CancellationToken cancellationToken)
    {
        var gate = mapper.Map<Gate>(request.Dto);
        await unitOfWork.Gates.AddAsync(gate);
        await unitOfWork.CompleteAsync();
        return Result<int>.Success(gate.Id);
    }
}
