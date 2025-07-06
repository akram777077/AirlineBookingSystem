using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Command.Delete;

public class DeleteFlightCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteFlightCommand, Result>
{
    public async Task<Result> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await unitOfWork.Flights.GetByIdAsync(request.Id);
        if (flight == null)
            return Result.NotFound("Flight not found.");

        unitOfWork.Flights.Delete(flight);
        await unitOfWork.CompleteAsync();

        return Result.Success(ResultStatusCode.NoContent);
    }
}