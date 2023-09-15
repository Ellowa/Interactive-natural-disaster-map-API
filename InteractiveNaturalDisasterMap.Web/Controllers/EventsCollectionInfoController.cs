using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.CreateEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.DeleteEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.UpdateEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Queries.GetAllEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Queries.GetByIdEventsCollectionInfo;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsCollectionInfoController : BaseController
    {
        // GET: api/EventsCollectionInfo
        [HttpGet]
        public async Task<IEnumerable<EventsCollectionInfoDto>> Get([FromQuery] GetAllEventsCollectionInfoDto getAllEventsCollectionInfoDto)
        {
            var request = new GetAllEventsCollectionInfoByUserIdRequest()
            {
                GetAllEventsCollectionInfoDto = getAllEventsCollectionInfoDto,
                UserId = (int)UserId!,
            };
            return await Mediator.Send(request);
        }

        // GET api/EventsCollectionInfo/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventsCollectionInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdEventsCollectionInfoRequest()
            {
                GetByIdEventsCollectionInfoDto = new GetByIdEventsCollectionInfoDto() { Id = id },
            };
            var eventsCollectionInfoDto = await Mediator.Send(request);
            return Ok(eventsCollectionInfoDto);
        }

        // POST api/EventsCollectionInfo
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateEventsCollectionInfoDto createEventsCollectionInfoDto)
        {
            var request = new CreateEventsCollectionInfoRequest()
            {
                CreateEventsCollectionInfoDto = createEventsCollectionInfoDto,
                UserId = (int)UserId!,
            };
            int createdEventsCollectionInfoId = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = createdEventsCollectionInfoId }, createdEventsCollectionInfoId);
        }

        // PUT api/EventsCollectionInfo/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventsCollectionInfoDto updateEventsCollectionInfoDto)
        {
            if (id != updateEventsCollectionInfoDto.Id) return BadRequest();

            var request = new UpdateEventsCollectionInfoRequest()
            {
                UpdateEventsCollectionInfoDto = updateEventsCollectionInfoDto,
                UserId = (int)UserId!,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/EventsCollectionInfo/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteEventsCollectionInfoRequest()
            {
                DeleteEventsCollectionInfoDto = new DeleteEventsCollectionInfoDto() { Id = id },
                UserId = (int)UserId!,
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
