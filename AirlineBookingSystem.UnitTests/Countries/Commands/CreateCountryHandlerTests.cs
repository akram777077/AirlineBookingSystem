using System.Threading;
using System.Threading.Tasks;
using AirlineBookingSystem.Application.CQRS.Countries.Commands;
using AirlineBookingSystem.Application.CQRS.Countries.Commands.Create;
using AirlineBookingSystem.Application.Interfaces.Repositories;
using AirlineBookingSystem.Domain.Entities;
using Moq;
using Xunit;

namespace AirlineBookingSystem.UnitTests.Countries.Commands
{
    public class CreateCountryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsCountryId()
        {
            // Arrange
            var mockRepo = new Mock<ICountryRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Country>()))
                .Callback<Country>(country => country.Id = 1)
                .Returns(Task.CompletedTask);

            var handler = new CreateCountryHandler(mockRepo.Object);
            var command = new CreateCountryCommand { Name = "TestCountry", Code = "TC" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public Task Handle_InvalidName_ThrowsValidationException()
        {
            // Arrange
            var mockRepo = new Mock<ICountryRepository>();
            var handler = new CreateCountryHandler(mockRepo.Object);
            var validator = new CreateCountryCommandValidator();
            var invalidCommand = new CreateCountryCommand { Name = "", Code = "TC" };

            // Act & Assert
            var result = validator.Validate(invalidCommand);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
            return Task.CompletedTask;
        }

        [Fact]
        public Task Handle_InvalidCode_ThrowsValidationException()
        {
            // Arrange
            var mockRepo = new Mock<ICountryRepository>();
            var handler = new CreateCountryHandler(mockRepo.Object);
            var validator = new CreateCountryCommandValidator();
            var invalidCommand = new CreateCountryCommand { Name = "ValidName", Code = "t1" };

            // Act & Assert
            var result = validator.Validate(invalidCommand);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Code");
            return Task.CompletedTask;
        }

        [Fact]
        public Task Handle_EmptyCommand_ThrowsValidationException()
        {
            // Arrange
            var mockRepo = new Mock<ICountryRepository>();
            var handler = new CreateCountryHandler(mockRepo.Object);
            var validator = new CreateCountryCommandValidator();
            var invalidCommand = new CreateCountryCommand { Name = "", Code = "" };

            // Act & Assert
            var result = validator.Validate(invalidCommand);
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
            Assert.Contains(result.Errors, e => e.PropertyName == "Code");
            return Task.CompletedTask;
        }
    }
}
