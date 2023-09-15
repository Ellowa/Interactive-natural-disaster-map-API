using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator { get { return _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;} }

        protected int? UserId
        {
            get 
            { 
                return !User.Identity.IsAuthenticated 
                ? null 
                : Convert.ToInt32(User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);
            }
        }
    }
}
