using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.CreateEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.DeleteEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.UpdateEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Queries.GetAllEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Queries.GetByIdEventSource;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class EventSourcesTests : BaseIntegrationTest
    {
        [Test]
        public async Task CreateEventSourceHandlerTest_WhenRequestIsValid_ShouldCreateEventSource()
        {
            // Arrange
            var request = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = "Test" },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            DbContext.EventSources.Should().Contain(x => x.Id == result);
        }

        [Test]
        public void CreateEventSourceHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task DeleteEventSourceHandlerTest_WhenEventSourceIsExistsAndUnknownSourceIsExists_ShouldDeleteEventSource()
        {
            // Arrange
            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = EntityNamesByDefault.DefaultEventSource },
            };
            await Mediator.Send(createEventSourceRequest);

            createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = "Test" },
            };
            var eventSourceId = await Mediator.Send(createEventSourceRequest);
            var expectedEventSourcesCount = DbContext.EventSources.Count() - 1;

            var request = new DeleteEventSourceRequest()
            {
                DeleteEventSourceDto = new DeleteEventSourceDto { Id = eventSourceId },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventSources.Count().Should().Be(expectedEventSourcesCount);
        }

        [Test]
        public async Task DeleteEventSourceHandlerTest_WhenEventSourceIsExistsAndUnknownSourceIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = "Test" },
            };
            var eventSourceId = await Mediator.Send(createEventSourceRequest);

            var request = new DeleteEventSourceRequest()
            {
                DeleteEventSourceDto = new DeleteEventSourceDto { Id = eventSourceId },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void DeleteEventSourceHandlerTest_WhenEventSourceIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteEventSourceRequest()
            {
                DeleteEventSourceDto = new DeleteEventSourceDto { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task UpdateEventSourceHandlerTest_WhenEventSourceIsExistsAndRequestIsValid_ShouldUpdateEventSource()
        {
            // Arrange
            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = "Test" },
            };
            var eventSourceId = await Mediator.Send(createEventSourceRequest);

            var request = new UpdateEventSourceRequest()
            {
                UpdateEventSourceDto = new UpdateEventSourceDto() { Id = eventSourceId, SourceType = "New" },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventSources.FirstOrDefault(ec => ec.SourceType == "New").Should().NotBeNull();
        }

        [Test]
        public void UpdateEventSourceHandlerTest_WhenEventSourceIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateEventSourceRequest()
            {
                UpdateEventSourceDto = new UpdateEventSourceDto() { Id = 1, SourceType = "New" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void UpdateEventSourceHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateEventSourceRequest()
            {
                UpdateEventSourceDto = new UpdateEventSourceDto() { Id = 1, SourceType = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task GetAllEventSourceHandlerTest_WhenEventSourceIsExists_ShouldReturnAllEventSources()
        {
            // Arrange
            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = "Test" },
            };
            await Mediator.Send(createEventSourceRequest);
            createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = "Test2" },
            };
            await Mediator.Send(createEventSourceRequest);

            var expectedEventSourcesCount = DbContext.EventSources.Count();

            var request = new GetAllEventSourceRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedEventSourcesCount);
        }

        [Test]
        public async Task GetAllEventSourceHandlerTest_WhenEventSourceIsNotExists_ShouldReturnZeroEventSources()
        {
            // Arrange
            var expectedEventSourcesCount = 0;

            var request = new GetAllEventSourceRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedEventSourcesCount);
        }


        [Test]
        public async Task GetByIdEventSourceHandlerTest_WhenEventSourceIsExists_ShouldReturnEventSource()
        {
            // Arrange
            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto { SourceType = "Test" },
            };
            var eventSourceId = await Mediator.Send(createEventSourceRequest);

            var request = new GetByIdEventSourceRequest()
            {
                GetByIdEventSourceDto = new GetByIdEventSourceDto() { Id = eventSourceId },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.SourceType.Should().Be("Test");
        }

        [Test]
        public void GetByIdEventSourceHandlerTest_WhenEventSourceIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdEventSourceRequest()
            {
                GetByIdEventSourceDto = new GetByIdEventSourceDto() { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
