﻿using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.AddMagnitudeUnitToEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.CreateMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.DeleteMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.DeleteMagnitudeUnitFromEventCategory;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Commands.UpdateMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.DTOs;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Queries.GetAllMagnitudeUnit;
using InteractiveNaturalDisasterMap.Application.Handlers.MagnitudeUnits.Queries.GetByIdMagnitudeUnit;
using InteractiveNaturalDisasterMap.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveNaturalDisasterMap.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagnitudeUnitController : BaseController
    {
        // GET: api/MagnitudeUnit
        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
        public async Task<IEnumerable<MagnitudeUnitDto>> Get()
        {
            var request = new GetAllMagnitudeUnitRequest();
            return await Mediator.Send(request);
        }

        // GET api/MagnitudeUnit/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MagnitudeUnitDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}, {UserRoles.User}")]
        public async Task<IActionResult> GetById(int id)
        {
            var request = new GetByIdMagnitudeUnitRequest()
            {
                GetByIdMagnitudeUnitDto = new GetByIdMagnitudeUnitDto() { Id = id },
            };
            var magnitudeUnitDto = await Mediator.Send(request);
            return Ok(magnitudeUnitDto);
        }

        // POST api/MagnitudeUnit
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IActionResult> Create([FromBody] CreateMagnitudeUnitDto createMagnitudeUnitDto)
        {
            var request = new CreateMagnitudeUnitRequest()
            {
                CreateMagnitudeUnitDto = createMagnitudeUnitDto,
            };
            int createdMagnitudeUnitId = await Mediator.Send(request);
            return CreatedAtAction(nameof(GetById), new { id = createdMagnitudeUnitId }, createdMagnitudeUnitId);
        }

        // PUT api/MagnitudeUnit/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMagnitudeUnitDto updateMagnitudeUnitDto)
        {
            if (id != updateMagnitudeUnitDto.Id) return BadRequest();

            var request = new UpdateMagnitudeUnitRequest()
            {
                UpdateMagnitudeUnitDto = updateMagnitudeUnitDto,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // POST api/MagnitudeUnit/AddToCategory
        [HttpPost("AddToCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IActionResult> MagnitudeUnitAddToCategory([FromBody] MagnitudeUnitToEventCategoryDto addMagnitudeUnitToEventCategoryDto)
        {
            var request = new AddMagnitudeUnitToEventCategoryRequest()
            {
                AddMagnitudeUnitToEventCategoryDto = addMagnitudeUnitToEventCategoryDto,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/MagnitudeUnit/DeleteFromCategory
        [HttpDelete("DeleteFromCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IActionResult> MagnitudeUnitDeleteFromCategory([FromBody] MagnitudeUnitToEventCategoryDto deleteMagnitudeUnitToEventCategoryDto)
        {
            var request = new DeleteMagnitudeUnitFromEventCategoryRequest()
            {
                DeleteMagnitudeUnitFromEventCategoryDto = deleteMagnitudeUnitToEventCategoryDto,
            };
            await Mediator.Send(request);
            return NoContent();
        }

        // DELETE api/MagnitudeUnit/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = $"{UserRoles.Moderator}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteMagnitudeUnitRequest()
            {
                DeleteMagnitudeUnitDto = new DeleteMagnitudeUnitDto() { Id = id },
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
