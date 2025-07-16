using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;
using AirlineBookingSystem.Shared.Results.Error;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.UpdateGate;

public class UpdateGateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateGateCommand, Result>
{
    public async Task<Result> Handle(UpdateGateCommand request, CancellationToken cancellationToken)
    {
        var gate = await unitOfWork.Gates.GetByIdAsync(request.Id);
        if (gate == null)
        {
            return Result.Failure("Gate NotFound");
        }

        mapper.Map(request.Dto, gate);
        unitOfWork.Gates.Update(gate);
        await unitOfWork.CompleteAsync();
        return Result.Success();
    }
}
