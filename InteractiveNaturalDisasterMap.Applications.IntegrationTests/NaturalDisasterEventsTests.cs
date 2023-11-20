using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.CreateEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.DeleteNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.UpdateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetAllNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetByIdInThirdPartyApiNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetByIdNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class NaturalDisasterEventsTests : BaseIntegrationTest
    {
        [Test]
        public async Task CreateNaturalDisasterEventHandlerTest_WhenRequestIsValid_ShouldCreateNaturalDisasterEvent()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto() { SourceType = "user" },
            };
            await Mediator.Send(createEventSourceRequest);

            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var userId = DbContext.Users.FirstOrDefault(u => u.Login == "Test")!.Id;

            var request = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                UserId = userId,
                SourceName = "user"
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            DbContext.NaturalDisasterEvents.Should().Contain(x => x.Id == result);
        }

        [Test]
        public void CreateNaturalDisasterEventHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                {
                    Title = "",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                SourceName = "Test"
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task DeleteNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsExists_ShouldDeleteNaturalDisasterEvent()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto() { SourceType = "user" },
            };
            await Mediator.Send(createEventSourceRequest);

            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var userId = DbContext.Users.FirstOrDefault(u => u.Login == "Test")!.Id;

            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                UserId = userId,
                SourceName = "user"
            };
            var eventId = await Mediator.Send(createNaturalDisasterEventRequest);

            var expectedNaturalDisasterEventsCount = DbContext.NaturalDisasterEvents.Count() - 1;

            var request = new DeleteNaturalDisasterEventRequest()
            {
                DeleteNaturalDisasterEventDto = new DeleteNaturalDisasterEventDto { Id = eventId },
                UserId = userId,
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.NaturalDisasterEvents.Count().Should().Be(expectedNaturalDisasterEventsCount);
        }

        [Test]
        public void DeleteNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteNaturalDisasterEventRequest()
            {
                DeleteNaturalDisasterEventDto = new DeleteNaturalDisasterEventDto { Id = 1 },
                UserId = 1,
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task UpdateNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsExistsAndRequestIsValid_ShouldUpdateNaturalDisasterEvent()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto() { SourceType = "user" },
            };
            await Mediator.Send(createEventSourceRequest);

            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var userId = DbContext.Users.FirstOrDefault(u => u.Login == "Test")!.Id;

            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                UserId = userId,
                SourceName = "user"
            };
            var eventId = await Mediator.Send(createNaturalDisasterEventRequest);

            var request = new UpdateNaturalDisasterEventRequest()
            {
                UpdateNaturalDisasterEventDto = new UpdateNaturalDisasterEventDto()
                {
                    Id = eventId,
                    Title = "New",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                UserId = userId,
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.NaturalDisasterEvents.FirstOrDefault(ec => ec.Title == "New").Should().NotBeNull();
        }

        [Test]
        public void UpdateNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateNaturalDisasterEventRequest()
            {
                UpdateNaturalDisasterEventDto = new UpdateNaturalDisasterEventDto()
                {
                    Id = 1,
                    Title = "New",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void UpdateNaturalDisasterEventHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateNaturalDisasterEventRequest()
            {
                UpdateNaturalDisasterEventDto = new UpdateNaturalDisasterEventDto()
                {
                    Id = 1, Title = "", StartDate = DateTime.Today, EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit, Latitude = 10.1, Longitude = 1
                },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task GetAllNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsExists_ShouldReturnAllNaturalDisasterEvents()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto() { SourceType = "user" },
            };
            await Mediator.Send(createEventSourceRequest);

            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var userId = DbContext.Users.FirstOrDefault(u => u.Login == "Test")!.Id;

            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                UserId = userId,
                SourceName = "user"
            };
            await Mediator.Send(createNaturalDisasterEventRequest);

            createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                {
                    Title = "Test2",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                UserId = userId,
                SourceName = "user"
            };
            await Mediator.Send(createNaturalDisasterEventRequest);

            var expectedNaturalDisasterEventsCount = 2;

            var request = new GetAllNaturalDisasterEventRequest()
                { GetAllNaturalDisasterEventDto = new GetAllNaturalDisasterEventDto(), UserId = userId };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedNaturalDisasterEventsCount);
        }

        [Test]
        public async Task GetAllNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsNotExists_ShouldReturnZeroNaturalDisasterEvents()
        {
            // Arrange
            var expectedNaturalDisasterEventsCount = 0;

            var request = new GetAllNaturalDisasterEventRequest() { GetAllNaturalDisasterEventDto = new GetAllNaturalDisasterEventDto() };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedNaturalDisasterEventsCount);
        }


        [Test]
        public async Task GetByIdNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsExists_ShouldReturnNaturalDisasterEvent()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto() { SourceType = "user" },
            };
            await Mediator.Send(createEventSourceRequest);

            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var userId = DbContext.Users.FirstOrDefault(u => u.Login == "Test")!.Id;

            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                UserId = userId,
                SourceName = "user"
            };
            var eventId = await Mediator.Send(createNaturalDisasterEventRequest);

            var request = new GetByIdNaturalDisasterEventRequest()
            {
                GetByIdNaturalDisasterEventDto = new GetByIdNaturalDisasterEventDto() { Id = eventId },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Title.Should().Be("Test");
        }

        [Test]
        public void GetByIdNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdNaturalDisasterEventRequest()
            {
                GetByIdNaturalDisasterEventDto = new GetByIdNaturalDisasterEventDto() { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task GetByIdInThirdPartyApiNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsExists_ShouldReturnNaturalDisasterEvent()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            var createEventSourceRequest = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = new CreateEventSourceDto() { SourceType = "user" },
            };
            await Mediator.Send(createEventSourceRequest);

            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var userId = DbContext.Users.FirstOrDefault(u => u.Login == "Test")!.Id;

            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    Latitude = 10.1,
                    Longitude = 1
                },
                UserId = userId,
                SourceName = "user",
                IdInThirdPartyApi = "Test"
            }; 
            await Mediator.Send(createNaturalDisasterEventRequest);

            var request = new GetByIdInThirdPartyApiNaturalDisasterEventRequest()
            {
                GetByIdNaturalDisasterEventDto = new GetByIdInThirdPartyApiNaturalDisasterEventDto() { Id = "Test" },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Title.Should().Be("Test");
        }

        [Test]
        public void GetByIdInThirdPartyApiNaturalDisasterEventHandlerTest_WhenNaturalDisasterEventIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdInThirdPartyApiNaturalDisasterEventRequest()
            {
                GetByIdNaturalDisasterEventDto = new GetByIdInThirdPartyApiNaturalDisasterEventDto() { Id = "Test" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
