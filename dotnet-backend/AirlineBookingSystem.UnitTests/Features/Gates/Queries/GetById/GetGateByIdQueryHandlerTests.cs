using AirlineBookingSystem.Application.Features.Gates.Queries.GetById;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AutoMapper;
using FluentAssertions;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Gates.Queries.GetById;

public class GetGateByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetGateByIdQueryHandler _handler;

    public GetGateByIdQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetGateByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnGateDto_WhenGateExists()
    {
        // Arrange
        var gateId = 1;

        // Use factories to create fully initialized entities
        var airport = AirportFactory.GetAirportFaker(1).Generate(); // Assuming cityId 1
        var terminal = TerminalFactory.GetTerminalFaker(airport.Id).Generate();
        terminal.Airport = airport;

        var gate = GateFactory.GetGateFaker(terminal.Id).Generate();
        gate.Terminal = terminal;
        gate.Id = gateId; // Ensure the ID matches the one being queried

        var gateDto = new GateDto { Id = gateId, GateNumber = gate.GateNumber, TerminalId = gate.TerminalId, TerminalName = gate.Terminal.Name };

        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(gateId)).ReturnsAsync(gate);
        _mapperMock.Setup(m => m.Map<GateDto>(gate)).Returns(gateDto);

        // Act
        var result = await _handler.Handle(new GetGateByIdQuery(gateId), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(gateDto);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenGateDoesNotExist()
    {
        // Arrange
        var gateId = 1;

        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(gateId)).ReturnsAsync((Gate)null);

        // Act
        var result = await _handler.Handle(new GetGateByIdQuery(gateId), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Should().Be("Gate NotFound");
    }
}
