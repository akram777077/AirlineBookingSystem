using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetById;

public class GetFlightClassByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetFlightClassByIdQuery, Result<FlightClassDto>>
{
    public async Task<Result<FlightClassDto>> Handle(GetFlightClassByIdQuery request, CancellationToken cancellationToken)
    {
        var flightClass = await unitOfWork.FlightClasses.GetByIdAsync(request.Id);

        if (flightClass == null)
        {
            return Result<FlightClassDto>.NotFound("FlightClass not found.");
        }

        var flightClassDto = mapper.Map<FlightClassDto>(flightClass);
        return Result<FlightClassDto>.Success(flightClassDto);
    }
}
