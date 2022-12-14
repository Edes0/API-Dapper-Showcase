namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.MimboxLocation.CreateMimboxLocation;
using Mimbly.Application.Commands.MimboxLocation.DeleteMimboxLocation;
using Mimbly.Application.Commands.MimboxLocation.UpdateMimboxLocation;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;
using Mimbly.Application.Queries.MimboxLocation.GetAll;
using Mimbly.Application.Queries.MimboxLocation.GetById;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MimboxLocationController : BaseController
{
    public MimboxLocationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllMimboxLocationsVm>> GetAllMimboxLocations()
    {
        return Ok(await _mediator.Send(new GetAllMimboxLocationsQuery { }));
    }

    [HttpGet("{id:guid}", Name = "MimboxLocationById")]
    public async Task<ActionResult<MimboxLocationByIdVm>> FilterMimboxLocationsById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdMimboxLocationQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateMimboxLocation([FromBody] CreateMimboxLocationRequestDto createMimboxLocationRequestDto)
    {
        var createdMimboxLocation = await _mediator.Send(new CreateMimboxLocationCommand { CreateMimboxLocationRequest = createMimboxLocationRequestDto });

        return CreatedAtRoute("MimboxLocationById", new { createdMimboxLocation.Id }, createdMimboxLocation);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMimboxLocation([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteMimboxLocationCommand { Id = id });

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateMimboxLocation(Guid id, [FromBody] UpdateMimboxLocationRequestDto updateMimboxLocationRequestDto)
    {
        await _mediator.Send(new UpdateMimboxLocationCommand { UpdateMimboxLocationRequest = updateMimboxLocationRequestDto, Id = id });

        return Ok("Mimbox location updated successfully");
    }
}