using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.DeleteUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.UpdateUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Queries.GetAllUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Queries.GetByIdUserRole;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class UserRolesTests : BaseIntegrationTest
    {
        [Test]
        public async Task CreateUserRoleHandlerTest_WhenRequestIsValid_ShouldCreateUserRole()
        {
            // Arrange
            var request = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "Test" },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            DbContext.UserRoles.Should().Contain(x => x.Id == result);
        }

        [Test]
        public void CreateUserRoleHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task DeleteUserRoleHandlerTest_WhenUserRoleIsExists_ShouldDeleteUserRole()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "Test" },
            };
            var eventSourceId = await Mediator.Send(createUserRoleRequest);
            var expectedUserRolesCount = DbContext.UserRoles.Count() - 1;

            var request = new DeleteUserRoleRequest()
            {
                DeleteUserRoleDto = new DeleteUserRoleDto { Id = eventSourceId },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.UserRoles.Count().Should().Be(expectedUserRolesCount);
        }

        [Test]
        public void DeleteUserRoleHandlerTest_WhenUserRoleIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteUserRoleRequest()
            {
                DeleteUserRoleDto = new DeleteUserRoleDto { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task UpdateUserRoleHandlerTest_WhenUserRoleIsExistsAndRequestIsValid_ShouldUpdateUserRole()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "Test" },
            };
            var eventSourceId = await Mediator.Send(createUserRoleRequest);

            var request = new UpdateUserRoleRequest()
            {
                UpdateUserRoleDto = new UpdateUserRoleDto() { Id = eventSourceId, RoleName = "New" },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.UserRoles.FirstOrDefault(ec => ec.RoleName == "New").Should().NotBeNull();
        }

        [Test]
        public void UpdateUserRoleHandlerTest_WhenUserRoleIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateUserRoleRequest()
            {
                UpdateUserRoleDto = new UpdateUserRoleDto() { Id = 1, RoleName = "New" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void UpdateUserRoleHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateUserRoleRequest()
            {
                UpdateUserRoleDto = new UpdateUserRoleDto() { Id = 1, RoleName = "" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task GetAllUserRoleHandlerTest_WhenUserRoleIsExists_ShouldReturnAllUserRoles()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "Test" },
            };
            await Mediator.Send(createUserRoleRequest);
            createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "Test2" },
            };
            await Mediator.Send(createUserRoleRequest);

            var expectedUserRolesCount = DbContext.UserRoles.Count();

            var request = new GetAllUserRoleRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedUserRolesCount);
        }

        [Test]
        public async Task GetAllUserRoleHandlerTest_WhenUserRoleIsNotExists_ShouldReturnZeroUserRoles()
        {
            // Arrange
            var expectedUserRolesCount = 0;

            var request = new GetAllUserRoleRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedUserRolesCount);
        }


        [Test]
        public async Task GetByIdUserRoleHandlerTest_WhenUserRoleIsExists_ShouldReturnUserRole()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto { RoleName = "Test" },
            };
            var eventSourceId = await Mediator.Send(createUserRoleRequest);

            var request = new GetByIdUserRoleRequest()
            {
                GetByIdUserRoleDto = new GetByIdUserRoleDto() { Id = eventSourceId },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.RoleName.Should().Be("Test");
        }

        [Test]
        public void GetByIdUserRoleHandlerTest_WhenUserRoleIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdUserRoleRequest()
            {
                GetByIdUserRoleDto = new GetByIdUserRoleDto() { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
