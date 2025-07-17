using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.terminals;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Queries.GetById;

public class GetTerminalByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetTerminalByIdQuery, Result<TerminalDto>>
{
    public async Task<Result<TerminalDto>> Handle(GetTerminalByIdQuery request, CancellationToken cancellationToken)
    {
        var terminal = await unitOfWork.Terminals.GetByIdAsync(request.Id);
        if (terminal == null)
            return Result<TerminalDto>.Failure("Terminal not found", ResultStatusCode.NotFound);

        var terminalDto = mapper.Map<TerminalDto>(terminal);
        return Result<TerminalDto>.Success(terminalDto);
    }
}
