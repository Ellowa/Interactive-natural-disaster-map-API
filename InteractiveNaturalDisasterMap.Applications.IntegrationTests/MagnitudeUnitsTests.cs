using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.CreateEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.AddMagnitudeUnitToEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.CreateMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.DeleteMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.UpdateMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Queries.GetAllMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Queries.GetByIdMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class MagnitudeUnitsTests : BaseIntegrationTest
    {
        [Test]
        public async Task AddMagnitudeUnitToEventCategoryHandlerTest_WhenRequestIsValid_ShouldAddMagnitudeUnitToEventCategory()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            var request = new AddMagnitudeUnitToEventCategoryRequest()
            {
                AddMagnitudeUnitToEventCategoryDto = new MagnitudeUnitToEventCategoryDto()
                    { MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit, EventCategoryName = EntityNamesByDefault.DefaultEventCategory },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventsCategories.Include(eventCategory => eventCategory.MagnitudeUnits)
                .FirstOrDefault(x => x.CategoryName == EntityNamesByDefault.DefaultEventCategory)!.MagnitudeUnits.Should()
                .Contain(x => x.MagnitudeUnitName == EntityNamesByDefault.DefaultMagnitudeUnit);
        }


        [Test]
        public async Task CreateMagnitudeUnitHandlerTest_WhenRequestIsValid_ShouldCreateMagnitudeUnit()
        {
            // Arrange
            var request = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "" },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            DbContext.MagnitudeUnits.Should().Contain(x => x.Id == result);
        }

        [Test]
        public void CreateMagnitudeUnitHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto { MagnitudeUnitName = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task DeleteMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsExistsAndUndefinedMagnitudeUnitIsExists_ShouldDeleteMagnitudeUnit()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto
                    { MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit, MagnitudeUnitDescription = "" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var createEventHazardUnitRequest = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = new CreateEventHazardUnitDto
                    { HazardName = EntityNamesByDefault.DefaultEventHazardUnit, MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit },
            };
            await Mediator.Send(createEventHazardUnitRequest);

            createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "" },
            };
            var magnitudeUnitId = await Mediator.Send(createMagnitudeUnitRequest);
            var expectedMagnitudeUnitsCount = DbContext.MagnitudeUnits.Count() - 1;

            var request = new DeleteMagnitudeUnitRequest()
            {
                DeleteMagnitudeUnitDto = new DeleteMagnitudeUnitDto { Id = magnitudeUnitId },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.MagnitudeUnits.Count().Should().Be(expectedMagnitudeUnitsCount);
        }

        [Test]
        public async Task DeleteMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsExistsAndUndefinedMagnitudeUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "" },
            };
            var magnitudeUnitId = await Mediator.Send(createMagnitudeUnitRequest);

            var request = new DeleteMagnitudeUnitRequest()
            {
                DeleteMagnitudeUnitDto = new DeleteMagnitudeUnitDto { Id = magnitudeUnitId },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void DeleteMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteMagnitudeUnitRequest()
            {
                DeleteMagnitudeUnitDto = new DeleteMagnitudeUnitDto { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task UpdateMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsExistsAndRequestIsValid_ShouldUpdateMagnitudeUnit()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "" },
            };
            var magnitudeUnitId = await Mediator.Send(createMagnitudeUnitRequest);

            var request = new UpdateMagnitudeUnitRequest()
            {
                UpdateMagnitudeUnitDto = new UpdateMagnitudeUnitDto()
                    { Id = magnitudeUnitId, MagnitudeUnitName = "New", MagnitudeUnitDescription = "New" },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.MagnitudeUnits.FirstOrDefault(ec => ec.MagnitudeUnitName == "New").Should().NotBeNull();
        }

        [Test]
        public void UpdateMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateMagnitudeUnitRequest()
            {
                UpdateMagnitudeUnitDto = new UpdateMagnitudeUnitDto() { Id = 1, MagnitudeUnitName = "New", MagnitudeUnitDescription = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void UpdateMagnitudeUnitHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateMagnitudeUnitRequest()
            {
                UpdateMagnitudeUnitDto = new UpdateMagnitudeUnitDto() { Id = 1, MagnitudeUnitName = "", MagnitudeUnitDescription = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task GetAllMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsExists_ShouldReturnAllMagnitudeUnits()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);
            createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto { MagnitudeUnitName = "Test2", MagnitudeUnitDescription = "" },
            };
            await Mediator.Send(createMagnitudeUnitRequest);

            var expectedMagnitudeUnitsCount = DbContext.MagnitudeUnits.Count();

            var request = new GetAllMagnitudeUnitRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedMagnitudeUnitsCount);
        }

        [Test]
        public async Task GetAllMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsNotExists_ShouldReturnZeroMagnitudeUnits()
        {
            // Arrange
            var expectedMagnitudeUnitsCount = 0;

            var request = new GetAllMagnitudeUnitRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedMagnitudeUnitsCount);
        }


        [Test]
        public async Task GetByIdMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsExists_ShouldReturnMagnitudeUnit()
        {
            // Arrange
            var createMagnitudeUnitRequest = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = new CreateMagnitudeUnitDto { MagnitudeUnitName = "Test", MagnitudeUnitDescription = "" },
            };
            var magnitudeUnitId = await Mediator.Send(createMagnitudeUnitRequest);

            var request = new GetByIdMagnitudeUnitRequest()
            {
                GetByIdMagnitudeUnitDto = new GetByIdMagnitudeUnitDto() { Id = magnitudeUnitId },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.MagnitudeUnitName.Should().Be("Test");
        }

        [Test]
        public void GetByIdMagnitudeUnitHandlerTest_WhenMagnitudeUnitIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdMagnitudeUnitRequest()
            {
                GetByIdMagnitudeUnitDto = new GetByIdMagnitudeUnitDto() { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
