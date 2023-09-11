using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.CreateEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.DeleteEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Commands.UpdateEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetAllEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.EventCategories.Queries.GetByIdEventCategory;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoryController : BaseController
    {
        // GET: api/EventCategory
        [HttpGet]
        public async Task<IEnumerable<EventCategoryDto>> Get()
        {
            var request = new GetAllEventCategoryRequest();
            return await Mediator.Send(request);
        }

        // GET api/EventCategory/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdEventCategoryRequest()
            {
                GetByIdEventCategoryDto = new GetByIdEventCategoryDto() { Id = id },
            };
            var eventCategoryDto = await Mediator.Send(request);
            return Ok(eventCategoryDto);
        }

        // POST api/EventCategory
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateEventCategoryDto createEventCategoryDto)
        {
            var request = new CreateEventCategoryRequest()
            {
                CreateEventCategoryDto = createEventCategoryDto,
            };
            int createdEventCategoryId = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = createdEventCategoryId }, createdEventCategoryId);
        }

        // PUT api/EventCategory/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventCategoryDto updateEventCategoryDto)
        {
            if (id != updateEventCategoryDto.Id) return BadRequest();

            var request = new UpdateEventCategoryRequest()
            {
                UpdateEventCategoryDto = updateEventCategoryDto,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/EventCategory/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteEventCategoryRequest()
            {
                DeleteEventCategoryDto = new DeleteEventCategoryDto() { Id = id },
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
