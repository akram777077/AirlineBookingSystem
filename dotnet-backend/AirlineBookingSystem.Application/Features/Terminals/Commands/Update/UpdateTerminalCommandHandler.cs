using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.Update;

/// <summary>
/// Handles the update of an existing terminal.
/// </summary>
public class UpdateTerminalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateTerminalCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="UpdateTerminalCommand"/> to update an existing terminal.
    /// This involves validating the associated airport and mapping the DTO to a Terminal entity.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(UpdateTerminalCommand request, CancellationToken cancellationToken)
    {
        var terminal = await unitOfWork.Terminals.GetByIdAsync(request.Dto.Id);
        if (terminal == null)
            return Result.Failure("Terminal not found", ResultStatusCode.NotFound);

        var airport = await unitOfWork.Airports.GetByIdAsync(request.Dto.AirportId);
        if (airport == null)
            return Result.Failure("Airport not found", ResultStatusCode.NotFound);

        mapper.Map(request.Dto, terminal);
        terminal.Airport = airport;

        unitOfWork.Terminals.Update(terminal);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.NoContent);
    }
}
