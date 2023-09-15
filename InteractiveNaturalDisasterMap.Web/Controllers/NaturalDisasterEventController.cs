using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.DeleteNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.UpdateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetAllNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetByIdNaturalDisasterEvent;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NaturalDisasterEventController : BaseController
    {
        // GET: api/NaturalDisasterEvent
        [HttpGet]
        public async Task<IEnumerable<NaturalDisasterEventDto>> Get([FromQuery]GetAllNaturalDisasterEventDto getAllNaturalDisasterEventDto)
        {
            var request = new GetAllNaturalDisasterEventRequest()
            {
                GetAllNaturalDisasterEventDto = getAllNaturalDisasterEventDto,
                UserId = UserId,
            };
            return await Mediator.Send(request);
        }

        // GET api/NaturalDisasterEvent/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(NaturalDisasterEventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdNaturalDisasterEventRequest()
            {
                GetByIdNaturalDisasterEventDto = new GetByIdNaturalDisasterEventDto() { Id = id },
            };
            var naturalDisasterEventDto = await Mediator.Send(request);
            return Ok(naturalDisasterEventDto);
        }

        // POST api/NaturalDisasterEvent
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateNaturalDisasterEventDto createNaturalDisasterEventDto)
        {
            var request = new CreateNaturalDisasterEventRequest()
            {
                CreateNaturalDisasterEventDto = createNaturalDisasterEventDto,
                SourceName = "User",
                UserId = UserId,
            };
            int createdNaturalDisasterEventId = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = createdNaturalDisasterEventId }, createdNaturalDisasterEventId);
        }

        // PUT api/NaturalDisasterEvent/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateNaturalDisasterEventDto updateNaturalDisasterEventDto)
        {
            if (id != updateNaturalDisasterEventDto.Id) return BadRequest();

            var request = new UpdateNaturalDisasterEventRequest()
            {
                UpdateNaturalDisasterEventDto = updateNaturalDisasterEventDto,
                UserId = (int)UserId!,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/NaturalDisasterEvent/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteNaturalDisasterEventRequest()
            {
                DeleteNaturalDisasterEventDto = new DeleteNaturalDisasterEventDto() { Id = id },
                UserId = (int)UserId!,
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
