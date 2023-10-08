using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.CreateEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.UpdateEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Queries.GetAllEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Queries.GetByIdEventsCollectionInfo;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.AddToEventsCollection;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteFromEventsCollection;
using InteractiveNaturalDisasterMap.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsCollectionController : BaseController
    {
        // GET: api/EventsCollection
        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
        public async Task<IEnumerable<EventsCollectionInfoDto>> Get([FromQuery] GetAllEventsCollectionInfoDto getAllEventsCollectionInfoDto)
        {
            var request = new GetAllEventsCollectionInfoByUserIdRequest()
            {
                GetAllEventsCollectionInfoDto = getAllEventsCollectionInfoDto,
                UserId = (int)UserId!,
            };
            return await Mediator.Send(request);
        }

        // GET api/EventsCollection/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventsCollectionInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdEventsCollectionInfoRequest()
            {
                GetByIdEventsCollectionInfoDto = new GetByIdEventsCollectionInfoDto() { Id = id },
            };
            var eventsCollectionInfoDto = await Mediator.Send(request);
            return Ok(eventsCollectionInfoDto);
        }

        // POST api/EventsCollection
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
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

        // PUT api/EventsCollection/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
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

        // DELETE api/EventsCollection/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
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

        // POST api/EventsCollection/AddEvent
        [HttpPost("AddEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
        public async Task<IActionResult> AddEventToCollection([FromBody] AddToEventsCollectionDto addToEventsCollectionDto)
        {
            var request = new AddToEventsCollectionRequest()
            {
                AddToEventsCollectionDto = addToEventsCollectionDto,
                UserId = (int)UserId!,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/EventsCollection/DeleteEvent
        [HttpDelete("DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
        public async Task<IActionResult> DeleteEventFromCollection([FromBody] DeleteFromEventsCollectionDto deleteFromEventsCollectionDto)
        {
            var request = new DeleteFromEventsCollectionRequest()
            {
                DeleteFromEventsCollectionDto = deleteFromEventsCollectionDto,
                UserId = (int)UserId!,
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
