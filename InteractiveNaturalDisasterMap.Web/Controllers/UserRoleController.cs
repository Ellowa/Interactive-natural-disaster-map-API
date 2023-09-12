using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.DeleteUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.UpdateUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Queries.GetAllUserRole;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Queries.GetByIdUserRole;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : BaseController
    {
        // GET: api/UserRole
        [HttpGet]
        public async Task<IEnumerable<UserRoleDto>> Get()
        {
            var request = new GetAllUserRoleRequest();
            return await Mediator.Send(request);
        }

        // GET api/UserRole/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserRoleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdUserRoleRequest()
            {
                GetByIdUserRoleDto = new GetByIdUserRoleDto() { Id = id },
            };
            var userRoleDto = await Mediator.Send(request);
            return Ok(userRoleDto);
        }

        // POST api/UserRole
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateUserRoleDto createUserRoleDto)
        {
            var request = new CreateUserRoleRequest()
            {
                CreateUserRoleDto = createUserRoleDto,
            };
            int createdUserRoleId = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = createdUserRoleId }, createdUserRoleId);
        }

        // PUT api/UserRole/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRoleDto updateUserRoleDto)
        {
            if (id != updateUserRoleDto.Id) return BadRequest();

            var request = new UpdateUserRoleRequest()
            {
                UpdateUserRoleDto = updateUserRoleDto,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/UserRole/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteUserRoleRequest()
            {
                DeleteUserRoleDto = new DeleteUserRoleDto() { Id = id },
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
