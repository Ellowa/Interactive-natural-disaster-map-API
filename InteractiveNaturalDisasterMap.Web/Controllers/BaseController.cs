using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator { get { return _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;} }
    }
}
