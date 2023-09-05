using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.CreateEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.DeleteEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Commands.UpdateEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Queries.GetAllEventHazardUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.Queries.GetByIdEventHazardUnit;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    public class EventHazardUnitController : BaseController
    {
        // GET: api/EventHazardUnit
        [HttpGet]
        public async Task<IEnumerable<EventHazardUnitDto>> Get()
        {
            var request = new GetAllEventHazardUnitRequest();
            return await Mediator.Send(request);
        }

        // GET api/EventHazardUnit/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventHazardUnitDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdEventHazardUnitRequest()
            {
                GetByIdEventHazardUnitDto = new GetByIdEventHazardUnitDto(){Id = id},
            };
            var hazardUnitDto = await Mediator.Send(request);
            return Ok(hazardUnitDto);
        }

        // POST api/EventHazardUnit
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateEventHazardUnitDto createEventHazardUnitDto)
        {
            var request = new CreateEventHazardUnitRequest()
            {
                CreateEventHazardUnitDto = createEventHazardUnitDto,
            };
            int createdHazardUnitId = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new {id = createdHazardUnitId}, createdHazardUnitId);
        }

        // PUT api/EventHazardUnit/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventHazardUnitDto updateEventHazardUnitDto)
        {
            if (id != updateEventHazardUnitDto.Id) return BadRequest();

            var request = new UpdateEventHazardUnitRequest()
            {
                UpdateEventHazardUnitDto = updateEventHazardUnitDto,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/EventHazardUnit/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteEventHazardUnitRequest()
            {
                DeleteEventHazardUnitDto = new DeleteEventHazardUnitDto(){Id = id},
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
