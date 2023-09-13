using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.ConfirmUnconfirmedEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetAllUnconfirmedEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetByIdUnconfirmedEvent;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnconfirmedEventController : BaseController
    {
        // GET: api/UnconfirmedEvent
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UnconfirmedEventDto>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<UnconfirmedEventDto>> Get([FromQuery]GetAllUnconfirmedEventDto getAllUnconfirmedEventDto)
        {
            var request = new GetAllUnconfirmedEventRequest()
            {
                GetAllUnconfirmedEventDto = getAllUnconfirmedEventDto,
            };
            return await Mediator.Send(request);
        }

        // GET api/UnconfirmedEvent/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UnconfirmedEventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdUnconfirmedEventRequest()
            {
                GetByIdUnconfirmedEventDto = new GetByIdUnconfirmedEventDto() { EventId = id },
            };
            var unconfirmedEvent = await Mediator.Send(request);
            return Ok(unconfirmedEvent);
        }

        // PATH api/UnconfirmedEvent/confirm/5
        [HttpPatch("confirm/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Confirm(int id, [FromBody] ConfirmOrRejectUnconfirmedEventDto confirmOrRejectUnconfirmedEventDto)
        {
            if (id != confirmOrRejectUnconfirmedEventDto.EventId) return BadRequest();

            var confirmOrReject = new ConfirmOrRejectUnconfirmedEventRequest()
            {
                ConfirmUnconfirmedEventDto = confirmOrRejectUnconfirmedEventDto,
                Reject = false,
            };
            await Mediator.Send(confirmOrReject);
            return NoContent();
        }

        // PATH api/UnconfirmedEvent/reject/5
        [HttpPatch("reject/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Reject(int id, [FromBody] ConfirmOrRejectUnconfirmedEventDto confirmOrRejectUnconfirmedEventDto)
        {
            if (id != confirmOrRejectUnconfirmedEventDto.EventId) return BadRequest();

            var confirmOrReject = new ConfirmOrRejectUnconfirmedEventRequest()
            {
                ConfirmUnconfirmedEventDto = confirmOrRejectUnconfirmedEventDto,
                Reject = true,
            };
            await Mediator.Send(confirmOrReject);
            return NoContent();
        }
    }
}
