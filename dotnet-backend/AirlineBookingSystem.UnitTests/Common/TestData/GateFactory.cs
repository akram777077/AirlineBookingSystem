using AirlineBookingSystem.Domain.Entities;
using Bogus;

namespace AirlineBookingSystem.UnitTests.Common.TestData;

public static class GateFactory
{
    public static Faker<Gate> GetGateFaker(int terminalId)
    {
        return new Faker<Gate>()
            .RuleFor(g => g.GateNumber, f => f.Finance.Account(2))
            .RuleFor(g => g.TerminalId, terminalId);
    }
}