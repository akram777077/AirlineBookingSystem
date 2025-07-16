using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using AutoMapper;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetAll;

public class GetAllFlightStatusesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllFlightStatusesQuery, Result<IEnumerable<FlightStatusDto>>>
{
    public async Task<Result<IEnumerable<FlightStatusDto>>> Handle(GetAllFlightStatusesQuery request, CancellationToken cancellationToken)
    {
        var flightStatuses = await unitOfWork.FlightStatuses.GetAllAsync();
        return Result<IEnumerable<FlightStatusDto>>.Success(mapper.Map<IEnumerable<FlightStatusDto>>(flightStatuses));
    }
}
