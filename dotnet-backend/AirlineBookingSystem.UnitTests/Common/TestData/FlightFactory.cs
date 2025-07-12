using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class FlightFactory
{
    public static Faker<Flight> GetFlightFaker(int airplaneId, int arrivalGateId, int departureGateId, int flightStatusId)
    {
        return new Faker<Flight>()
            .RuleFor(f => f.FlightNumber, f => f.Finance.Account(6))
            .RuleFor(f => f.DepartureTime, f => f.Date.Future())
            .RuleFor(f => f.ArrivalTime, f => f.Date.Future())
            .RuleFor(f => f.AirplaneId, airplaneId)
            .RuleFor(f => f.ArrivalGateId, arrivalGateId)
            .RuleFor(f => f.DepartureGateId, departureGateId)
            .RuleFor(f => f.FlightStatusId, flightStatusId)
            .RuleFor(f => f.Airplane, f => AirplaneFactory.GetAirplaneFaker().Generate())
            .RuleFor(f => f.ArrivalGate, f => GateFactory.GetGateFaker(f.Random.Int(1, 10)).Generate())
            .RuleFor(f => f.DepartureGate, f => GateFactory.GetGateFaker(f.Random.Int(1, 10)).Generate())
            .RuleFor(f => f.FlightStatus, f => FlightStatusFactory.GetFlightStatusFaker().Generate());
    }
}