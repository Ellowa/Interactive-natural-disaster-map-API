using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.DeleteUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.SetModeratorPermission;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.UpdateUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetAllUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByIdUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByLoginUser;
using InteractiveNaturalDisasterMap.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        // GET: api/User
        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IEnumerable<UserDto>> Get()
        {
            var request = new GetAllUserRequest();
            return await Mediator.Send(request);
        }

        // GET api/User/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdUserRequest()
            {
                GetByIdUserDto = new GetByIdUserDto() { Id = id },
                UserId = (int)UserId!,
            };
            var userDto = await Mediator.Send(request);
            return Ok(userDto);
        }

        // GET api/User/login
        [HttpGet("{login:alpha}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            var request = new GetByLoginUserRequest()
            {
                GetByLoginUserDto = new GetByLoginUserDto() { Login = login },
            };
            var userDto = await Mediator.Send(request);
            return Ok(userDto);
        }

        // POST api/User
        [HttpPost, AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            var request = new CreateUserRequest()
            {
                CreateUserDto = createUserDto,
            };
            string createdUserJwt = await Mediator.Send(request);
            return Ok(createdUserJwt);
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id) return BadRequest();

            var request = new UpdateUserRequest()
            {
                UpdateUserDto = updateUserDto,
                UserId = (int)UserId!,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteUserRequest()
            {
                DeleteUserDto = new DeleteUserDto() { Id = id },
                UserId = (int)UserId!,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // POST api/User/5
        [HttpPost("sertmoderator/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IActionResult> SetModeratorPermission(int id)
        {

            var request = new SetModeratorPermissionRequest()
            {
                SetModeratorPermissionDto = new() { Id = id }
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
