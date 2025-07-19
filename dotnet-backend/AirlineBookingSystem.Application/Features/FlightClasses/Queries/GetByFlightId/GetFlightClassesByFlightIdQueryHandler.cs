using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flightClasses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;


namespace AirlineBookingSystem.Application.Features.FlightClasses.Queries.GetByFlightId;

public class GetFlightClassesByFlightIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetFlightClassesByFlightIdQuery, Result<IEnumerable<FlightClassDto>>>
{
    public async Task<Result<IEnumerable<FlightClassDto>>> Handle(GetFlightClassesByFlightIdQuery request, CancellationToken cancellationToken)
    {
        var flightClasses = (await unitOfWork.FlightClasses.GetAllAsync())
            .Where(fc => fc.FlightId == request.FlightId)
            .ToList();

        if (!flightClasses.Any())
        {
            return Result<IEnumerable<FlightClassDto>>.NotFound($"No flight classes found for FlightId: {request.FlightId}.");
        }

        var flightClassDtos = mapper.Map<IEnumerable<FlightClassDto>>(flightClasses);
        return Result<IEnumerable<FlightClassDto>>.Success(flightClassDtos);
    }
}
