using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
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
    }
}
