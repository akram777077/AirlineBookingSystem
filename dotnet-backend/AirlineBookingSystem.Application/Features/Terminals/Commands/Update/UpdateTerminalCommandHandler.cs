using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Terminals.Commands.Update;

public class UpdateTerminalCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateTerminalCommand, Result>
{
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
