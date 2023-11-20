using FluentAssertions;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.DeleteUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.LoginUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.SetModeratorPermission;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.UpdateUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetAllUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByIdUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByLoginUser;
using InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests
{
    public class UsersTests : BaseIntegrationTest
    {
        [Test]
        public async Task CreateUserHandlerTest_WhenRequestIsValid_ShouldCreateUser()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var request = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.Users.Should().Contain(x => x.Login == "Test");
        }

        [Test]
        public async Task CreateUserHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);

            var request = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "", Password = "Test_123", RefreshToken = "" }
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }


        [Test]
        public async Task DeleteUserHandlerTest_WhenUserIsExists_ShouldDeleteUser()
        {
            // Arrange
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

            var expectedUsersCount = DbContext.Users.Count() - 1;

            var request = new DeleteUserRequest()
            {
                DeleteUserDto = new DeleteUserDto { Id = userId },
                UserId = userId,
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.Users.Count().Should().Be(expectedUsersCount);
        }

        [Test]
        public async Task DeleteUserHandlerTest_WhenUserTryDeleteAnotherUser_ShouldThrowAuthorizationException()
        {
            // Arrange
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

            createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test2", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var user2Id = DbContext.Users.FirstOrDefault(u => u.Login == "Test2")!.Id;

            var request = new DeleteUserRequest()
            {
                DeleteUserDto = new DeleteUserDto { Id = userId },
                UserId = user2Id,
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<AuthorizationException>(Action);
        }

        [Test]
        public async Task DeleteUserHandlerTest_WhenModeratorTryDeleteAnotherUser_ShouldDeleteUser()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);
            createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "moderator" },
            };
            await Mediator.Send(createUserRoleRequest);

            var createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var userId = DbContext.Users.FirstOrDefault(u => u.Login == "Test")!.Id;

            createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Admin", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var moderatorId = DbContext.Users.FirstOrDefault(u => u.Login == "Admin")!.Id;

            var setModeratorPermissionRequest = new SetModeratorPermissionRequest() { SetModeratorPermissionDto = new SetModeratorPermissionDto() { Id = moderatorId } };
            await Mediator.Send(setModeratorPermissionRequest);

            var expectedUsersCount = DbContext.Users.Count() - 1;

            var request = new DeleteUserRequest()
            {
                DeleteUserDto = new DeleteUserDto { Id = userId },
                UserId = moderatorId,
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.Users.Count().Should().Be(expectedUsersCount);
        }

        [Test]
        public void DeleteUserHandlerTest_WhenUserIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new DeleteUserRequest()
            {
                DeleteUserDto = new DeleteUserDto { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task SetModeratorPermissionHandlerTest_WhenModeratorRoleIsExists_ShouldSetModeratorPermission()
        {
            // Arrange
            var createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "user" },
            };
            await Mediator.Send(createUserRoleRequest);
            createUserRoleRequest = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = new CreateUserRoleDto() { RoleName = "moderator" },
            };
            var moderatorRoleId = await Mediator.Send(createUserRoleRequest);

            var createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var userId = DbContext.Users.FirstOrDefault(u => u.Login == "Test")!.Id;

            var request = new SetModeratorPermissionRequest()
            {
                SetModeratorPermissionDto = new SetModeratorPermissionDto() { Id = userId },
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.Users.FirstOrDefault(x => x.Login == "Test")!.RoleId.Should().Be(moderatorRoleId);
        }

        [Test]
        public async Task SetModeratorPermissionHandlerTest_WhenModeratorRoleIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
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

            var request = new SetModeratorPermissionRequest()
            {
                SetModeratorPermissionDto = new SetModeratorPermissionDto() { Id = userId },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }


        [Test]
        public async Task LoginUserHandlerTest_WhenUserIsExists_ShouldReturnJwt()
        {
            // Arrange
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

            var request = new LoginUserRequest()
            {
                LoginUserDto = new LoginUserDto(){Login = "Test", Password = "Test_123"}
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void LoginUserHandlerTest_WhenUserIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new LoginUserRequest()
            {
                LoginUserDto = new LoginUserDto() { Login = "Test", Password = "Test_123" }
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public async Task LoginUserHandlerTest_WhenPasswordIncorrect_ShouldThrowRequestArgumentException()
        {
            // Arrange
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

            var request = new LoginUserRequest()
            {
                LoginUserDto = new LoginUserDto() { Login = "Test", Password = "Test" }
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<RequestArgumentException>(Action);
        }


        [Test]
        public async Task UpdateUserHandlerTest_WhenUserIsExistsAndRequestIsValid_ShouldUpdateUser()
        {
            // Arrange
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

            var request = new UpdateUserRequest()
            {
                UpdateUserDto = new UpdateUserDto() { Id = userId, Login = "NewLogin", Password = "NewPass_123"},
                UserId = userId,
            };

            // Act
            await Mediator.Send(request);

            // Assert
            DbContext.Users.FirstOrDefault(ec => ec.Login == "NewLogin").Should().NotBeNull();
        }

        [Test]
        public void UpdateUserHandlerTest_WhenUserIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new UpdateUserRequest()
            {
                UpdateUserDto = new UpdateUserDto() { Id = 1, Login = "NewLogin", Password = "NewPass_123" },
                UserId = 1,
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public void UpdateUserHandlerTest_WhenRequestIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var request = new UpdateUserRequest()
            {
                UpdateUserDto = new UpdateUserDto() { Id = 1, Login = "NewLogin", Password = "New" },
                UserId = 1,
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<ValidationException>(Action);
        }

        [Test]
        public async Task UpdateUserHandlerTest_WhenUserTryUpdateAnotherUser_ShouldThrowAuthorizationException()
        {
            // Arrange
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

            createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test2", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var user2Id = DbContext.Users.FirstOrDefault(u => u.Login == "Test2")!.Id;

            var request = new UpdateUserRequest()
            {
                UpdateUserDto = new UpdateUserDto() { Id = userId, Login = "NewLogin", Password = "NewPass_123" },
                UserId = user2Id,
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<AuthorizationException>(Action);
        }


        [Test]
        public async Task GetAllUserHandlerTest_WhenUserIsExists_ShouldReturnAllUsers()
        {
            // Arrange
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

            createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test2", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);

            var expectedUsersCount = DbContext.Users.Count();

            var request = new GetAllUserRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedUsersCount);
        }

        [Test]
        public async Task GetAllUserHandlerTest_WhenUserIsNotExists_ShouldReturnZeroUsers()
        {
            // Arrange
            var expectedUsersCount = 0;

            var request = new GetAllUserRequest();

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Count.Should().Be(expectedUsersCount);
        }


        [Test]
        public async Task GetByIdUserHandlerTest_WhenUserIsExists_ShouldReturnUser()
        {
            // Arrange
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

            var request = new GetByIdUserRequest()
            {
                GetByIdUserDto = new GetByIdUserDto() { Id = userId },
                UserId = userId,
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Login.Should().Be("Test");
        }

        [Test]
        public void GetByIdUserHandlerTest_WhenUserIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByIdUserRequest()
            {
                GetByIdUserDto = new GetByIdUserDto() { Id = 1 },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }

        [Test]
        public async Task GetByIdUserHandlerTest_WhenUserTryToGetAnotherUser_ShouldThrowAuthorizationException()
        {
            // Arrange
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

            createUserRequest = new CreateUserRequest()
            {
                CreateUserDto = new CreateUserDto { Login = "Test2", Password = "Test_123", RefreshToken = "" }
            };
            await Mediator.Send(createUserRequest);
            var user2Id = DbContext.Users.FirstOrDefault(u => u.Login == "Test2")!.Id;

            var request = new GetByIdUserRequest()
            {
                GetByIdUserDto = new GetByIdUserDto() { Id = userId },
                UserId = user2Id,
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<AuthorizationException>(Action);
        }


        [Test]
        public async Task GetByLoginUserHandlerTest_WhenUserIsExists_ShouldReturnUser()
        {
            // Arrange
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

            var request = new GetByLoginUserRequest()
            {
                GetByLoginUserDto = new GetByLoginUserDto() { Login = "Test" },
            };

            // Act
            var result = await Mediator.Send(request);

            // Assert
            result.Login.Should().Be("Test");
        }

        [Test]
        public void GetByLoginUserHandlerTest_WhenUserIsNotExists_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = new GetByLoginUserRequest()
            {
                GetByLoginUserDto = new GetByLoginUserDto() { Login = "Test" },
            };

            // Act
            Task Action() => Mediator.Send(request);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Action);
        }
    }
}
