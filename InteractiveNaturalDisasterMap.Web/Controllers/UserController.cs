using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.DeleteUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.UpdateUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetAllUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Queries.GetByIdUser;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        // GET: api/User
        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            var request = new GetAllUserRequest();
            return await Mediator.Send(request);
        }

        // GET api/User/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdUserRequest()
            {
                GetByIdUserDto = new GetByIdUserDto() { Id = id },
            };
            var userDto = await Mediator.Send(request);
            return Ok(userDto);
        }

        // POST api/User
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            var request = new CreateUserRequest()
            {
                CreateUserDto = createUserDto,
            };
            int createdUserId = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = createdUserId }, createdUserId);
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id) return BadRequest();

            var request = new UpdateUserRequest()
            {
                UpdateUserDto = updateUserDto,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteUserRequest()
            {
                DeleteUserDto = new DeleteUserDto() { Id = id },
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
