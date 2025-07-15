using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Query.ById;

public class GetAirportByIdQuery(int id) : IRequest<Result<AirportDto>>
{
    public int Id => id;
}