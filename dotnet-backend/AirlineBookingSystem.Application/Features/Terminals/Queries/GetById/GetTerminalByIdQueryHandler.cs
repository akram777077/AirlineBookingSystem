using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.GetById;

/// <summary>
/// Handles the retrieval of a terminal by its unique identifier.
/// </summary>
public class GetTerminalByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetTerminalByIdQuery, Result<TerminalDto>>
{
    /// <summary>
    /// Handles the <see cref="GetTerminalByIdQuery"/> to retrieve a terminal by its ID.
    /// </summary>
    /// <param name="request">The query to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{TerminalDto}"/> indicating the success or failure of the operation, with the terminal DTO on success.</returns>
    public async Task<Result<TerminalDto>> Handle(GetTerminalByIdQuery request, CancellationToken cancellationToken)
    {
        var terminal = await unitOfWork.Terminals.GetByIdAsync(request.Id);
        if (terminal == null)
            return Result.Failure<TerminalDto>("Terminal not found", ResultStatusCode.NotFound);

        var terminalDto = mapper.Map<TerminalDto>(terminal);
        return Result<TerminalDto>.Success(terminalDto);
    }
}
