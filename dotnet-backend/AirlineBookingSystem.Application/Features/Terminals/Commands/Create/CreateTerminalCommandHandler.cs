using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.Create;

/// <summary>
/// Handles the creation of a new terminal.
/// </summary>
public class CreateTerminalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateTerminalCommand, Result<int>>
{
    /// <summary>
    /// Handles the <see cref="CreateTerminalCommand"/> to create a new terminal.
    /// This involves validating the associated airport and mapping the DTO to a Terminal entity.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{Int32}"/> indicating the success or failure of the operation, with the ID of the created terminal on success.</returns>
    public async Task<Result<int>> Handle(CreateTerminalCommand request, CancellationToken cancellationToken)
    {
        var airport = await unitOfWork.Airports.GetByIdAsync(request.Dto.AirportId);
        if (airport == null)
                        return Result.Failure<int>("Airport not found", ResultStatusCode.NotFound);

        var terminal = mapper.Map<Terminal>(request.Dto);
        terminal.Airport = airport;

        await unitOfWork.Terminals.AddAsync(terminal);
        await unitOfWork.CompleteAsync();

        return Result.Success(terminal.Id, ResultStatusCode.Created);
    }
    }
