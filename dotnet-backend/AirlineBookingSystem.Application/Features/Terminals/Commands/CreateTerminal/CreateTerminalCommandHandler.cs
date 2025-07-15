using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.Enums;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.CreateTerminal;

public class CreateTerminalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateTerminalCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateTerminalCommand request, CancellationToken cancellationToken)
    {
        var airport = await unitOfWork.Airports.GetByIdAsync(request.Dto.AirportId);
        if (airport == null)
            return Result<int>.Failure("Airport not found", ResultStatusCode.NotFound);

        var terminal = mapper.Map<Terminal>(request.Dto);
        terminal.Airport = airport;

        await unitOfWork.Terminals.AddAsync(terminal);
        await unitOfWork.CompleteAsync();

        return Result<int>.Success(terminal.Id, ResultStatusCode.Created);
    }
}
