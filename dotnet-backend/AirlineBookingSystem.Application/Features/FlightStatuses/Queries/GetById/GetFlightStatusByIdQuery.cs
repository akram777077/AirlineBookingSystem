using AirlineBookingSystem.Shared.DTOs.FlightStatuses;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.FlightStatuses.Queries.GetById;

public class GetFlightStatusByIdQuery(int id) : IRequest<Result<FlightStatusDto>>
{
    public int Id => id;
}
