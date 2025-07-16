using AirlineBookingSystem.Application.Features.Gates.Commands.CreateGate;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Moq;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Gates.Commands.CreateGate;

public class CreateGateCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateGateCommandHandler _handler;

    public CreateGateCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateGateCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_CreateGate_WhenValidData()
    {
        // Arrange
        var createGateDto = new CreateGateDto { GateNumber = "G10", TerminalId = 1 };

        // Use factories to create fully initialized entities
        var airport = AirportFactory.GetAirportFaker(1).Generate(); // Assuming cityId 1
        var terminal = TerminalFactory.GetTerminalFaker(airport.Id).Generate();
        terminal.Airport = airport; // Manually assign the generated airport

        var gate = GateFactory.GetGateFaker(terminal.Id).Generate();
        gate.Terminal = terminal; // Manually assign the generated terminal

        // Ensure the mapped gate has the correct properties from the DTO
        gate.GateNumber = createGateDto.GateNumber;
        gate.TerminalId = createGateDto.TerminalId;

        _mapperMock.Setup(m => m.Map<Gate>(createGateDto)).Returns(gate);
        _unitOfWorkMock.Setup(u => u.Gates.AddAsync(gate)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(new CreateGateCommand(createGateDto), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(gate.Id);
        _unitOfWorkMock.Verify(u => u.Gates.AddAsync(gate), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }
}
