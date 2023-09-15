using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.CreateEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.DeleteEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.UpdateEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Queries.GetAllEventSource;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Queries.GetByIdEventSource;
using InteractiveNaturalDisasterMap.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{UserRoles.Moderator}")]
    public class EventSourceController : BaseController
    {
        // GET: api/EventSource
        [HttpGet]
        public async Task<IEnumerable<EventSourceDto>> Get()
        {
            var request = new GetAllEventSourceRequest();
            return await Mediator.Send(request);
        }

        // GET api/EventSource/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventSourceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdEventSourceRequest()
            {
                GetByIdEventSourceDto = new GetByIdEventSourceDto() { Id = id },
            };
            var eventSourceDto = await Mediator.Send(request);
            return Ok(eventSourceDto);
        }

        // POST api/EventSource
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateEventSourceDto createEventSourceDto)
        {
            var request = new CreateEventSourceRequest()
            {
                CreateEventSourceDto = createEventSourceDto,
            };
            int createdEventSourceId = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = createdEventSourceId }, createdEventSourceId);
        }

        // PUT api/EventSource/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventSourceDto updateEventSourceDto)
        {
            if (id != updateEventSourceDto.Id) return BadRequest();

            var request = new UpdateEventSourceRequest()
            {
                UpdateEventSourceDto = updateEventSourceDto,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/EventSource/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteEventSourceRequest()
            {
                DeleteEventSourceDto = new DeleteEventSourceDto() { Id = id },
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
