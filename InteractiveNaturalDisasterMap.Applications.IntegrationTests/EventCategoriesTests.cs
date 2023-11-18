using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.DeleteEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.UpdateEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetAllEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetByIdEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.CreateMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class EventCategoriesTests : BaseIntegrationTest
    {
        [Test]
        public async Task CreateEventCategoryHandlerTest_WhenRequestIsValidAndUndefinedMagnitudeUnitIsExists_ShouldCreateEventCategory()
        {
            // Arrange
            var request = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };
            
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "undefined", MagnitudeUnitDescription = "Test"},
            };
            await Mediator.Send(createMagnitudeUnitRequest);
            
            // Act
            var result = await Mediator.Send(request);
            
            // Assert
            DbContext.EventsCategories.Should().Contain(x => x.Id == result);
        }

        [Test]
        public void CreateEventCategoryHandlerTest_WhenRequestIsValidAndUndefinedMagnitudeUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void CreateEventCategoryHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "" },
            };
            
            // Act
            Task Action() => Mediator.Send(request);
            
            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task DeleteEventCategoryHandlerTest_WhenEventCategoryIsExistsAndOtherCategoryIsExists_ShouldDeleteEventCategory()
        {
            // Arrange
            var createEventCategoryRequest = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "other" },
            };

            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "undefined", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);
            await Mediator.Send(createEventCategoryRequest);

            createEventCategoryRequest = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };
            var eventCategoryId = await Mediator.Send(createEventCategoryRequest);
            var expectedEventsCategoriesCount = DbContext.EventsCategories.Count() - 1;

            var request = new DeleteEventCategoryRequest()
            {
                DeleteEventCategoryDto = new DeleteEventCategoryDto { Id = eventCategoryId },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventsCategories.Count().Should().Be(expectedEventsCategoriesCount);
        }

        [Test]
        public async Task DeleteEventCategoryHandlerTest_WhenEventCategoryIsExistsAndOtherCategoryIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "undefined", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventCategoryRequest = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };
            var eventCategoryId = await Mediator.Send(createEventCategoryRequest);

            var request = new DeleteEventCategoryRequest()
            {
                DeleteEventCategoryDto = new DeleteEventCategoryDto { Id = eventCategoryId },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void DeleteEventCategoryHandlerTest_WhenEventCategoryIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteEventCategoryRequest()
            {
                DeleteEventCategoryDto = new DeleteEventCategoryDto { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task UpdateEventCategoryHandlerTest_WhenEventCategoryIsExistsAndRequestIsValid_ShouldUpdateEventCategory()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "undefined", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventCategoryRequest = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };
            var eventCategoryId = await Mediator.Send(createEventCategoryRequest);

            var request = new UpdateEventCategoryRequest()
            {
                UpdateEventCategoryDto = new UpdateEventCategoryDto() { Id = eventCategoryId, CategoryName = "New"},
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventsCategories.FirstOrDefault(ec => ec.CategoryName == "New").Should().NotBeNull();
        }

        [Test]
        public void UpdateEventCategoryHandlerTest_WhenEventCategoryIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateEventCategoryRequest()
            {
                UpdateEventCategoryDto = new UpdateEventCategoryDto() { Id = 1, CategoryName = "New" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void UpdateEventCategoryHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateEventCategoryRequest()
            {
                UpdateEventCategoryDto = new UpdateEventCategoryDto() { Id = 1, CategoryName = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task GetAllEventCategoryHandlerTest_WhenEventCategoryIsExists_ShouldReturnAllEventCategories()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "undefined", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventCategoryRequest = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };
            await Mediator.Send(createEventCategoryRequest);
            createEventCategoryRequest = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test2" },
            };
            await Mediator.Send(createEventCategoryRequest);

            var expectedEventsCategoriesCount = DbContext.EventsCategories.Count();

            var request = new GetAllEventCategoryRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedEventsCategoriesCount);
        }

        [Test]
        public async Task GetAllEventCategoryHandlerTest_WhenEventCategoryIsNotExists_ShouldReturnZeroEventCategories()
        {
            // Arrange
            var expectedEventsCategoriesCount = 0;

            var request = new GetAllEventCategoryRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedEventsCategoriesCount);
        }


        [Test]
        public async Task GetByIdEventCategoryHandlerTest_WhenEventCategoryIsExists_ShouldReturnEventCategory()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto() { MagnitudeUnitName = "undefined", MagnitudeUnitDescription = "Test" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventCategoryRequest = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = new CreateEventCategoryDto { CategoryName = "Test" },
            };
            var eventCategoryId = await Mediator.Send(createEventCategoryRequest);

            var request = new GetByIdEventCategoryRequest()
            {
                GetByIdEventCategoryDto = new GetByIdEventCategoryDto() { Id = eventCategoryId },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.CategoryName.Should().Be("Test");
        }

        [Test]
        public void GetByIdEventCategoryHandlerTest_WhenEventCategoryIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdEventCategoryRequest()
            {
                GetByIdEventCategoryDto = new GetByIdEventCategoryDto() { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
