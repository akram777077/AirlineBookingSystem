using AirlineBookingSystem.Application.Features.Gates.Commands.Update;
using AirlineBookingSystem.Application.Interfaces.UnitOfWork;
using AirlineBookingSystem.Domain.Entities;
using AirlineBookingSystem.Shared.DTOs.Gates;
using AutoMapper;
using FluentAssertions;
using Moq;
using AirlineBookingSystem.UnitTests.Common.TestData;

namespace AirlineBookingSystem.UnitTests.Features.Gates.Commands.UpdateGate;

public class UpdateGateCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UpdateGateCommandHandler _handler;

    public UpdateGateCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _handler = new UpdateGateCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_UpdateGate_WhenGateExists()
    {
        // Arrange
        var gateId = 1;
        var updateGateDto = new UpdateGateDto { Id = gateId, GateNumber = "G10-Updated", TerminalId = 2 };

        var airport = AirportFactory.GetAirportFaker(1).Generate(); // Assuming cityId 1
        var terminal = TerminalFactory.GetTerminalFaker(airport.Id).Generate();
        terminal.Airport = airport;

        var gate = GateFactory.GetGateFaker(terminal.Id).Generate();
        gate.Terminal = terminal;
        gate.Id = gateId; // Ensure the ID matches the one being updated

        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(gateId)).ReturnsAsync(gate);
        _mapperMock.Setup(m => m.Map(updateGateDto, gate)).Verifiable();
        _unitOfWorkMock.Setup(u => u.Gates.Update(gate)).Verifiable();
        _unitOfWorkMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(new UpdateGateCommand(gateId, updateGateDto), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _unitOfWorkMock.Verify(u => u.Gates.GetByIdAsync(gateId), Times.Once);
        _mapperMock.Verify(m => m.Map(updateGateDto, gate), Times.Once);
        _unitOfWorkMock.Verify(u => u.Gates.Update(gate), Times.Once);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenGateDoesNotExist()
    {
        // Arrange
        var gateId = 1;
        var updateGateDto = new UpdateGateDto { Id = gateId, GateNumber = "G10-Updated", TerminalId = 2 };

        _unitOfWorkMock.Setup(u => u.Gates.GetByIdAsync(gateId)).ReturnsAsync((Gate)null);

        // Act
        var result = await _handler.Handle(new UpdateGateCommand(gateId, updateGateDto), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Should().Be("Gate NotFound");
        _unitOfWorkMock.Verify(u => u.Gates.Update(It.IsAny<Gate>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Never);
    }
}
