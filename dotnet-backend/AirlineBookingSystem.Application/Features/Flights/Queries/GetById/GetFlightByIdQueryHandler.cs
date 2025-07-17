using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.flights;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Queries.GetById;

public class GetFlightByIdQueryHandler (IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetFlightByIdQuery, Result<FlightDetailsDto>>
{
    public async Task<Result<FlightDetailsDto>> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
    {
        var flight = await unitOfWork.Flights.GetByIdAsync(request.Id);
        if (flight == null)
        {
            return Result<FlightDetailsDto>.Failure("Flight not found", ResultStatusCode.NotFound);
        }

        var flightDetailsDto = mapper.Map<FlightDetailsDto>(flight);
        return Result<FlightDetailsDto>.Success(flightDetailsDto);
    }
}