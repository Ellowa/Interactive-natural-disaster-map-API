using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.AddToEventsCollection;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Commands.DeleteFromEventsCollection;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using InteractiveNaturalDisasterMap.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsCollectionController : BaseController
    {
        // POST api/EventsCollection
        [HttpPost]
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

        // DELETE api/EventsCollection/5
        [HttpDelete]
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
