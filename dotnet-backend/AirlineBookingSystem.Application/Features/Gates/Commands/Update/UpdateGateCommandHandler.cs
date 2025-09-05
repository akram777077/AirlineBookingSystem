using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Update;

/// <summary>
/// Handles the update of an existing gate.
/// </summary>
public class UpdateGateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateGateCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="UpdateGateCommand"/> to update an existing gate.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
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
