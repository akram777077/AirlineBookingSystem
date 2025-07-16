using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;

public class GetFlightStatusByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetFlightStatusByIdQuery, Result<FlightStatusDto>>
{
    public async Task<Result<FlightStatusDto>> Handle(GetFlightStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var flightStatus = await unitOfWork.FlightStatuses.GetByIdAsync(request.Id);
        return flightStatus == null ? Result<FlightStatusDto>.NotFound("Flight status not found.") : Result<FlightStatusDto>.Success(mapper.Map<FlightStatusDto>(flightStatus));
    }
}
