using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Delete;

/// <summary>
/// Handles the deletion of a flight.
/// </summary>
public class DeleteFlightCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteFlightCommand, Result>
{
    /// <summary>
    /// Handles the <see cref="DeleteFlightCommand"/> to soft-delete a flight.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the deletion.</returns>
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