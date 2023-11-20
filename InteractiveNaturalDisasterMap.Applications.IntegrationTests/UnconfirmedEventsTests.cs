using FluentAssertions;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.CreateEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.ConfirmUnconfirmedEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetAllUnconfirmedEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetByIdUnconfirmedEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class UnconfirmedEventsTests : BaseIntegrationTest
    {
        [Test]
        public async Task ConfirmOrRejectUnconfirmedEventHandlerTest_WhenConfirmEvent_ShouldConfirmEvent()
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
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto()
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

            var request = new ConfirmOrRejectUnconfirmedEventRequest()
            {
                ConfirmUnconfirmedEventDto = new ConfirmOrRejectUnconfirmedEventDto()
                {
                    EventId = eventId,
                },
                Reject = false
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.UnconfirmedEvents.Should().NotContain(x => x.EventId == eventId);
            DbContext.NaturalDisasterEvents.FirstOrDefault(x => x.Id == eventId)!.Confirmed.Should().Be(true);
        }

        [Test]
        public async Task ConfirmOrRejectUnconfirmedEventHandlerTest_WhenRejectEvent_ShouldNotConfirmEvent()
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
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto()
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

            var request = new ConfirmOrRejectUnconfirmedEventRequest()
            {
                ConfirmUnconfirmedEventDto = new ConfirmOrRejectUnconfirmedEventDto()
                {
                    EventId = eventId,
                },
                Reject = true
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.UnconfirmedEvents.Should().Contain(x => x.EventId == eventId);
            DbContext.NaturalDisasterEvents.FirstOrDefault(x => x.Id == eventId)!.Confirmed.Should().Be(false);
        }

        [Test]
        public void ConfirmOrRejectUnconfirmedEventHandlerTest_WhenUnconfirmedEventIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new ConfirmOrRejectUnconfirmedEventRequest()
            {
                ConfirmUnconfirmedEventDto = new ConfirmOrRejectUnconfirmedEventDto()
                {
                    EventId = 1,
                },
                Reject = false
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }



        [Test]
        public async Task GetAllUnconfirmedEventHandlerTest_WhenUnconfirmedEventIsExists_ShouldReturnAllUnconfirmedEvents()
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
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto()
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

            var expectedUnconfirmedEventsCount = 2;

            var request = new GetAllUnconfirmedEventRequest()
                { GetAllUnconfirmedEventDto = new GetAllUnconfirmedEventDto() };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedUnconfirmedEventsCount);
        }

        [Test]
        public async Task GetAllUnconfirmedEventHandlerTest_WhenUnconfirmedEventIsNotExists_ShouldReturnZeroUnconfirmedEvents()
        {
            // Arrange
            var expectedUnconfirmedEventsCount = 0;

            var request = new GetAllUnconfirmedEventRequest() { GetAllUnconfirmedEventDto = new GetAllUnconfirmedEventDto() };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedUnconfirmedEventsCount);
        }


        [Test]
        public async Task GetByIdUnconfirmedEventHandlerTest_WhenUnconfirmedEventIsExists_ShouldReturnUnconfirmedEvent()
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

            var request = new GetByIdUnconfirmedEventRequest()
            {
                GetByIdUnconfirmedEventDto = new GetByIdUnconfirmedEventDto() { EventId = eventId },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.EventDto.Title.Should().Be("Test");
        }

        [Test]
        public void GetByIdUnconfirmedEventHandlerTest_WhenUnconfirmedEventIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdUnconfirmedEventRequest()
            {
                GetByIdUnconfirmedEventDto = new GetByIdUnconfirmedEventDto() { EventId = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
