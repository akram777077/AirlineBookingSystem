using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using MediatR;

namespace AirlineBookingSystem.Application.Features.Flights.Commands.Create;

public class CreateFlightCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateFlightCommand, int>
{
    public async Task<int> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var fromAirport = await unitOfWork.Airports.GetByIdAsync(request.FromAirportId);
        var toAirport = await unitOfWork.Airports.GetByIdAsync(request.ToAirportId);

        if (fromAirport == null || toAirport == null)
            throw new InvalidOperationException("One or both airports not found.");

        var flight = new Flight
        {
            FlightNumber = request.FlightNumber,
            FromAirportId = request.FromAirportId,
            FromAirport = fromAirport!,
            ToAirportId = request.ToAirportId,
            ToAirport = toAirport!,
            DepartureTime = request.DepartureTime,
            ArrivalTime = request.ArrivalTime
        };

        await unitOfWork.Flights.AddAsync(flight);
        await unitOfWork.CompleteAsync();
        return flight.Id;
    }
}
