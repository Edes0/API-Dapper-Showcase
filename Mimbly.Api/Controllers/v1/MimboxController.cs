namespace Mimbly.Api.Controllers.v1;

using Application.Commands.Mimbox.CreateMimbox;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.Mimbox.DeleteMimbox;
using Mimbly.Application.Commands.Mimbox.UpdateMimbox;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.Application.Queries.Mimbox.GetAll;
using Mimbly.Application.Queries.Mimbox.GetById;

[ApiController]
//[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MimboxController : BaseController
{
    public MimboxController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllMimboxesVm>> GetAllMimboxes()
    {
        return Ok(await _mediator.Send(new GetAllMimboxesQuery { }));
    }

    [HttpGet("{id:guid}", Name = "MimboxById")]
    public async Task<ActionResult<MimboxByIdVm>> FilterMimboxesById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdMimboxQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateMimbox([FromBody] CreateMimboxRequestDto createMimboxRequestDto)
    {
        await _mediator.Send(new CreateMimboxCommand { CreateMimboxRequest = createMimboxRequestDto });

        return CreatedAtRoute("MimboxById", createMimboxRequestDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMimbox([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteMimboxCommand { Id = id });

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateMimbox(Guid id, [FromBody] UpdateMimboxRequestDto updateMimboxRequestDto)
    {
        await _mediator.Send(new UpdateMimboxCommand { UpdateMimboxRequest = updateMimboxRequestDto, Id = id });

        return Ok("Mimbox updated successfully");
    }
}