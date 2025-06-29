using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Update;

public class UpdateFlightCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateFlightCommand, bool>
{
    public async Task<bool> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await unitOfWork.Flights.GetByIdAsync(request.Id);
        if (flight == null)
            return false;

        var fromAirport = await unitOfWork.Airports.GetByIdAsync(request.FromAirportId);
        var toAirport = await unitOfWork.Airports.GetByIdAsync(request.ToAirportId);
        if (fromAirport == null || toAirport == null)
            throw new InvalidOperationException("One or both airports not found.");

        flight.FlightNumber = request.FlightNumber;
        flight.FromAirportId = request.FromAirportId;
        flight.FromAirport = fromAirport;
        flight.ToAirportId = request.ToAirportId;
        flight.ToAirport = toAirport;
        flight.DepartureTime = request.DepartureTime;
        flight.ArrivalTime = request.ArrivalTime;

        await unitOfWork.CompleteAsync();
        return true;
    }
}
