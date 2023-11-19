using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.CreateEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.DeleteEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.UpdateEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Queries.GetAllEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Queries.GetByIdEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.CreateMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class EventHazardUnitsTests : BaseIntegrationTest
    {
        [Test]
        public async Task CreateEventHazardUnitHandlerTest_WhenRequestIsValid_ShouldCreateEventHazardUnit()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var request = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto { HazardName = "Test", MagnitudeUnitName = "Test" },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            DbContext.EventHazardUnits.Should().Contain(x => x.Id == result);
        }

        [Test]
        public void CreateEventHazardUnitHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto { HazardName = "" },
            };
            
            // Act
            Task Action() => Mediator.Send(request);
            
            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task DeleteEventHazardUnitHandlerTest_WhenEventHazardUnitIsExistsAndUndefinedHazardUnitIsExists_ShouldDeleteEventHazardUnit()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventHazardUnitRequest = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto
                    { HazardName = EntityNamesByDefault.DefaultEventHazardUnit, MagnitudeUnitName = "Test" },
            };
            await Mediator.Send(createEventHazardUnitRequest);

            createEventHazardUnitRequest = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto { HazardName = "Test", MagnitudeUnitName = "Test" },
            };
            var eventHazardUnitId = await Mediator.Send(createEventHazardUnitRequest);
            var expectedEventHazardUnitsCount = DbContext.EventHazardUnits.Count() - 1;

            var request = new DeleteEventHazardUnitRequest()
            {
                DeleteEventHazardUnitDto = new DeleteEventHazardUnitDto { Id = eventHazardUnitId },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventHazardUnits.Count().Should().Be(expectedEventHazardUnitsCount);
        }

        [Test]
        public async Task DeleteEventHazardUnitHandlerTest_WhenEventHazardUnitIsExistsAndUndefinedHazardUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventHazardUnitRequest = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto { HazardName = "Test", MagnitudeUnitName = "Test"},
            };
            var eventHazardUnitId = await Mediator.Send(createEventHazardUnitRequest);

            var request = new DeleteEventHazardUnitRequest()
            {
                DeleteEventHazardUnitDto = new DeleteEventHazardUnitDto { Id = eventHazardUnitId },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void DeleteEventHazardUnitHandlerTest_WhenEventHazardUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteEventHazardUnitRequest()
            {
                DeleteEventHazardUnitDto = new DeleteEventHazardUnitDto { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task UpdateEventHazardUnitHandlerTest_WhenEventHazardUnitIsExistsAndRequestIsValid_ShouldUpdateEventHazardUnit()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventHazardUnitRequest = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto { HazardName = "Test", MagnitudeUnitName = "Test"},
            };
            var eventHazardUnitId = await Mediator.Send(createEventHazardUnitRequest);

            var request = new UpdateEventHazardUnitRequest()
            {
                UpdateEventHazardUnitDto = new UpdateEventHazardUnitDto() { Id = eventHazardUnitId, HazardName = "New", MagnitudeUnitName = "Test", ThresholdValue = 43},
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventHazardUnits.FirstOrDefault(ec => ec.HazardName == "New").Should().NotBeNull();
        }

        [Test]
        public void UpdateEventHazardUnitHandlerTest_WhenEventHazardUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateEventHazardUnitRequest()
            {
                UpdateEventHazardUnitDto = new UpdateEventHazardUnitDto() { Id = 1, HazardName = "New", MagnitudeUnitName = "New"},
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void UpdateEventHazardUnitHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateEventHazardUnitRequest()
            {
                UpdateEventHazardUnitDto = new UpdateEventHazardUnitDto() { Id = 1, MagnitudeUnitName = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task GetAllEventHazardUnitHandlerTest_WhenEventHazardUnitIsExists_ShouldReturnAllEventHazardUnits()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventHazardUnitRequest = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto { HazardName = "Test", MagnitudeUnitName = "Test"},
            };
            await Mediator.Send(createEventHazardUnitRequest);
            createEventHazardUnitRequest = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto { HazardName = "Test2", MagnitudeUnitName = "Test"},
            };
            await Mediator.Send(createEventHazardUnitRequest);

            var expectedEventHazardUnitsCount = DbContext.EventHazardUnits.Count();

            var request = new GetAllEventHazardUnitRequest(){GetAllEventHazardUnitDto = new GetAllEventHazardUnitDto()};

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedEventHazardUnitsCount);
        }

        [Test]
        public async Task GetAllEventHazardUnitHandlerTest_WhenEventHazardUnitIsNotExists_ShouldReturnZeroEventHazardUnits()
        {
            // Arrange
            var expectedEventHazardUnitsCount = 0;

            var request = new GetAllEventHazardUnitRequest() { GetAllEventHazardUnitDto = new GetAllEventHazardUnitDto() };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedEventHazardUnitsCount);
        }


        [Test]
        public async Task GetByIdEventHazardUnitHandlerTest_WhenEventHazardUnitIsExists_ShouldReturnEventHazardUnit()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventHazardUnitRequest = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto {HazardName = "Test", MagnitudeUnitName = "Test" },
            };
            var eventHazardUnitId = await Mediator.Send(createEventHazardUnitRequest);

            var request = new GetByIdEventHazardUnitRequest()
            {
                GetByIdEventHazardUnitDto = new GetByIdEventHazardUnitDto() { Id = eventHazardUnitId },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.HazardName.Should().Be("Test");
        }

        [Test]
        public void GetByIdEventHazardUnitHandlerTest_WhenEventHazardUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdEventHazardUnitRequest()
            {
                GetByIdEventHazardUnitDto = new GetByIdEventHazardUnitDto() { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
