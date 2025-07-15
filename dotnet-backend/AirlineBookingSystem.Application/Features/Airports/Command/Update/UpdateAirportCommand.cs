using AirlineBookingSystem.Shared.DTOs.airports;
using AirlineBookingSystem.Shared.Results;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Airports.Command.Update;

public class UpdateAirportCommand(UpdateAirportDto airport) : IRequest<Result<AirportDto>>
{
    public UpdateAirportDto Airport => airport;
}