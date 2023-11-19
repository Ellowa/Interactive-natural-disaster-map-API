using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.AddToEventsCollection;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.CreateEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteFromEventsCollection;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.UpdateEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Queries.GetAllEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Queries.GetByIdEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class EventsCollectionsTests : BaseIntegrationTest
    {
        [Test]
        public async Task AddToEventsCollectionHandlerTest_WhenEventsCollectionInfoIsExistsAndEventIsExists_ShouldAddEventToEventsCollection()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            // Create user
            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId, Login = "Test2", PasswordHash = new byte[] { 1, 2, 3 }, PasswordSalt = new byte[] { 1, 2, 3 }, JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            // Create createEventsCollectionInfo
            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = userId
            };
            var eventEventsCollectionInfoId = await Mediator.Send(createEventsCollectionInfoRequest);

            // Create event
            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto()
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    Latitude = 0,
                    Longitude = 0,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                },
                SourceName = EntityNamesByDefault.DefaultEventSource,
                UserId = userId
            };
            var eventId = await Mediator.Send(createNaturalDisasterEventRequest);

            var request = new AddToEventsCollectionRequest()
            {
                AddToEventsCollectionDto = new AddToEventsCollectionDto() { CollectionId = eventEventsCollectionInfoId, EventId = eventId },
                UserId = userId
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventsCollections.Should().Contain(x => x.CollectionId == eventEventsCollectionInfoId && x.EventId == eventId);
        }

        [Test]
        public async Task AddToEventsCollectionHandlerTest_WhenEventsCollectionInfoIsExistsAndEventIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            // Create user
            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            // Create createEventsCollectionInfo
            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = userId
            };
            var eventEventsCollectionInfoId = await Mediator.Send(createEventsCollectionInfoRequest);

            var request = new AddToEventsCollectionRequest()
            {
                AddToEventsCollectionDto = new AddToEventsCollectionDto() { CollectionId = eventEventsCollectionInfoId, EventId = 1 },
                UserId = userId
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public async Task AddToEventsCollectionHandlerTest_WhenEventsCollectionInfoIsNotExistsAndEventIsExists_ShouldThrowNotFoundException()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            // Create user
            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            // Create event
            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto()
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    Latitude = 0,
                    Longitude = 0,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                },
                SourceName = EntityNamesByDefault.DefaultEventSource,
                UserId = userId
            };
            var eventId = await Mediator.Send(createNaturalDisasterEventRequest);

            var request = new AddToEventsCollectionRequest()
            {
                AddToEventsCollectionDto = new AddToEventsCollectionDto() { CollectionId = 1, EventId = eventId },
                UserId = userId
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public async Task AddToEventsCollectionHandlerTest_WhenUserTryAddToNotOwnCollection_ShouldThrowAuthorizationException()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            // Create user
            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            // Create createEventsCollectionInfo
            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = 2
            };
            var eventEventsCollectionInfoId = await Mediator.Send(createEventsCollectionInfoRequest);

            // Create event
            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto()
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    Latitude = 0,
                    Longitude = 0,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                },
                SourceName = EntityNamesByDefault.DefaultEventSource,
                UserId = userId
            };
            var eventId = await Mediator.Send(createNaturalDisasterEventRequest);

            var request = new AddToEventsCollectionRequest()
            {
                AddToEventsCollectionDto = new AddToEventsCollectionDto() { CollectionId = eventEventsCollectionInfoId, EventId = eventId },
                UserId = userId
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<AuthorizationException>(Action);
        }


        [Test]
        public async Task CreateEventsCollectionInfoHandlerTest_WhenRequestIsValid_ShouldCreateEventsCollectionInfo()
        {
            // Arrange
            var request = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            DbContext.EventsCollectionsInfo.Should().Contain(x => x.Id == result);
        }

        [Test]
        public void CreateEventsCollectionInfoHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task DeleteEventsCollectionInfoHandlerTest_WhenEventsCollectionInfoIsExists_ShouldDeleteEventsCollectionInfo()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = userId
            };
            var eventEventsCollectionInfoId = await Mediator.Send(createEventsCollectionInfoRequest);
            var expectedEventsCollectionInfosCount = DbContext.EventsCollectionsInfo.Count() - 1;

            var request = new DeleteEventsCollectionInfoRequest()
            {
                DeleteEventsCollectionInfoDto = new DeleteEventsCollectionInfoDto { Id = eventEventsCollectionInfoId },
                UserId = userId
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventsCollectionsInfo.Count().Should().Be(expectedEventsCollectionInfosCount);
        }

        [Test]
        public void DeleteEventsCollectionInfoHandlerTest_WhenEventsCollectionInfoIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteEventsCollectionInfoRequest()
            {
                DeleteEventsCollectionInfoDto = new DeleteEventsCollectionInfoDto { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public async Task DeleteEventsCollectionInfoHandlerTest_WhenUserTryDeleteNotOwnCollection_ShouldThrowAuthorizationException()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = 2
            };
            var eventEventsCollectionInfoId = await Mediator.Send(createEventsCollectionInfoRequest);

            var request = new DeleteEventsCollectionInfoRequest()
            {
                DeleteEventsCollectionInfoDto = new DeleteEventsCollectionInfoDto { Id = eventEventsCollectionInfoId },
                UserId = userId
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<AuthorizationException>(Action);
        }


        [Test]
        public async Task
            DeleteFromEventsCollectionHandlerTest_WhenEventsCollectionInfoIsExistsAndEventIsExists_ShouldDeleteEventFromEventsCollection()
        {
            // Arrange
            TestsData.SeedData(DbContext);

            // Create user
            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            // Create createEventsCollectionInfo
            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = userId
            };
            var eventEventsCollectionInfoId = await Mediator.Send(createEventsCollectionInfoRequest);

            // Create event
            var createNaturalDisasterEventRequest = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto()
                {
                    Title = "Test",
                    StartDate = DateTime.Today,
                    Latitude = 0,
                    Longitude = 0,
                    MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit,
                    EventCategoryName = EntityNamesByDefault.DefaultEventCategory,
                },
                SourceName = EntityNamesByDefault.DefaultEventSource,
                UserId = userId
            };
            var eventId = await Mediator.Send(createNaturalDisasterEventRequest);

            var addToEventsCollectionRequest = new AddToEventsCollectionRequest()
            {
                AddToEventsCollectionDto = new AddToEventsCollectionDto() { CollectionId = eventEventsCollectionInfoId, EventId = eventId },
                UserId = userId
            };
            await Mediator.Send(addToEventsCollectionRequest);

            var expectedEventsCollectionCount = DbContext.EventsCollections.Count() - 1;

            var request = new DeleteFromEventsCollectionRequest()
            {
                DeleteFromEventsCollectionDto = new DeleteFromEventsCollectionDto() { CollectionId = eventEventsCollectionInfoId, EventId = eventId },
                UserId = userId
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventsCollections.Count().Should().Be(expectedEventsCollectionCount);
        }


        [Test]
        public async Task UpdateEventsCollectionInfoHandlerTest_WhenEventsCollectionInfoIsExistsAndRequestIsValid_ShouldUpdateEventsCollectionInfo()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = userId
            };
            var eventEventsCollectionInfoId = await Mediator.Send(createEventsCollectionInfoRequest);

            var request = new UpdateEventsCollectionInfoRequest()
            {
                UpdateEventsCollectionInfoDto = new UpdateEventsCollectionInfoDto() { Id = eventEventsCollectionInfoId, CollectionName = "New" },
                UserId = userId
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.EventsCollectionsInfo.FirstOrDefault(ec => ec.CollectionName == "New").Should().NotBeNull();
        }

        [Test]
        public void UpdateEventsCollectionInfoHandlerTest_WhenEventsCollectionInfoIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateEventsCollectionInfoRequest()
            {
                UpdateEventsCollectionInfoDto = new UpdateEventsCollectionInfoDto() { Id = 1, CollectionName = "New" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void UpdateEventsCollectionInfoHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateEventsCollectionInfoRequest()
            {
                UpdateEventsCollectionInfoDto = new UpdateEventsCollectionInfoDto() { Id = 1, CollectionName = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task GetAllEventsCollectionInfoHandlerTest_WhenEventsCollectionInfoIsExists_ShouldReturnAllEventsCollectionInfos()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = userId
            };
            await Mediator.Send(createEventsCollectionInfoRequest);
            createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test2" },
                UserId = userId
            };
            await Mediator.Send(createEventsCollectionInfoRequest);

            var expectedEventsCollectionInfosCount = DbContext.EventsCollectionsInfo.Count();

            var request = new GetAllEventsCollectionInfoByUserIdRequest()
                { UserId = userId, GetAllEventsCollectionInfoDto = new GetAllEventsCollectionInfoDto() { UserId = userId } };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedEventsCollectionInfosCount);
        }

        [Test]
        public async Task GetAllEventsCollectionInfoHandlerTest_WhenEventsCollectionInfoIsNotExists_ShouldReturnZeroEventsCollectionInfos()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            var expectedEventsCollectionInfosCount = 0;

            var request = new GetAllEventsCollectionInfoByUserIdRequest()
                { UserId = userId, GetAllEventsCollectionInfoDto = new GetAllEventsCollectionInfoDto() { UserId = userId } };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedEventsCollectionInfosCount);
        }


        [Test]
        public async Task GetByIdEventsCollectionInfoHandlerTest_WhenEventsCollectionInfoIsExists_ShouldReturnEventsCollectionInfo()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var userId = 1;
            DbContext.Users.Add(new User()
            {
                Id = userId,
                Login = "Test2",
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 1, 2, 3 },
                JwtRefreshToken = "",
                RoleId = DbContext.UserRoles.FirstOrDefault()!.Id
            });
            await DbContext.SaveChangesAsync();

            var createEventsCollectionInfoRequest = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = new CreateEventsCollectionInfoDto { CollectionName = "Test" },
                UserId = userId
            };
            var eventCollectionInfoId = await Mediator.Send(createEventsCollectionInfoRequest);

            var request = new GetByIdEventsCollectionInfoRequest()
            {
                GetByIdEventsCollectionInfoDto = new GetByIdEventsCollectionInfoDto() { Id = eventCollectionInfoId },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.CollectionName.Should().Be("Test");
        }

        [Test]
        public void GetByIdEventsCollectionInfoHandlerTest_WhenEventsCollectionInfoIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdEventsCollectionInfoRequest()
            {
                GetByIdEventsCollectionInfoDto = new GetByIdEventsCollectionInfoDto() { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
