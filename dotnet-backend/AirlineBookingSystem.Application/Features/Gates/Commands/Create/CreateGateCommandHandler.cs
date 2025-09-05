using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Gates.Commands.Create;

/// <summary>
/// Handles the creation of a new gate.
/// </summary>
public class CreateGateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateGateCommand, Result<int>>
{
    /// <summary>
    /// Handles the <see cref="CreateGateCommand"/> to create a new gate.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{Int32}"/> indicating the success or failure of the operation, with the ID of the created gate on success.</returns>
    public async Task<Result<int>> Handle(CreateGateCommand request, CancellationToken cancellationToken)
    {
        var gate = mapper.Map<Gate>(request.Dto);
        await unitOfWork.Gates.AddAsync(gate);
        await unitOfWork.CompleteAsync();
        return Result<int>.Success(gate.Id);
    }
}
