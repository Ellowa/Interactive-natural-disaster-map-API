using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.LoginUser;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var request = new LoginUserRequest()
            {
                LoginUserDto = loginUserDto,
            };

            string token = await Mediator.Send(request);

            return Ok(token);
        }
    }
}
