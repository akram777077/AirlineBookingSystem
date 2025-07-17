using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Commands.Update;

public class UpdateAirportCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateAirportCommand, Result<AirportDto>>
{
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