using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Commands.Update;

/// <summary>
/// Handles the update of an existing airport.
/// </summary>
public class UpdateAirportCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateAirportCommand, Result<AirportDto>>
{
    /// <summary>
    /// Handles the <see cref="UpdateAirportCommand"/> to update an existing airport.
    /// </summary>
    /// <param name="request">The command to handle.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Result{AirportDto}"/> indicating the success or failure of the operation, with the updated airport's DTO on success.</returns>
    public async Task<Result<AirportDto>> Handle(UpdateAirportCommand request, CancellationToken cancellationToken)
    {
        var airport = await unitOfWork.Airports.GetByIdAsync(request.Airport.Id);
        if (airport == null)
        {
            return Result<AirportDto>.NotFound("Airport not found.");
        }

        mapper.Map(request.Airport, airport);
        unitOfWork.Airports.Update(airport);
        await unitOfWork.CompleteAsync();

        return Result<AirportDto>.Success(mapper.Map<AirportDto>(airport));
    }
}