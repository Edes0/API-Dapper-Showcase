namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.MimboxStatus.CreateMimboxStatus;
using Mimbly.Application.Commands.MimboxStatus.DeleteMimboxStatus;
using Mimbly.Application.Commands.MimboxStatus.UpdateMimboxStatus;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;
using Mimbly.Application.Queries.MimboxStatus.GetAll;
using Mimbly.Application.Queries.MimboxStatus.GetById;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MimboxStatusController : BaseController
{
    public MimboxStatusController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllMimboxStatusesVm>> GetAllMimboxStatuss()
    {
        return Ok(await _mediator.Send(new GetAllMimboxStatusesQuery { }));
    }

    [HttpGet("{id:guid}", Name = "MimboxStatusById")]
    public async Task<ActionResult<MimboxStatusByIdVm>> FilterMimboxStatussById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdMimboxStatusQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateMimboxStatus([FromBody] CreateMimboxStatusRequestDto createMimboxStatusRequestDto)
    {
        var createdMimboxStatus = await _mediator.Send(new CreateMimboxStatusCommand { CreateMimboxStatusRequest = createMimboxStatusRequestDto });

        return CreatedAtRoute("MimboxStatusById", new { createdMimboxStatus.Id }, createdMimboxStatus);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMimboxStatus([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteMimboxStatusCommand { Id = id });

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateMimboxStatus(Guid id, [FromBody] UpdateMimboxStatusRequestDto updateMimboxStatusRequestDto)
    {
        await _mediator.Send(new UpdateMimboxStatusCommand { UpdateMimboxStatusRequest = updateMimboxStatusRequestDto, Id = id });

        return Ok("Mimbox status updated successfully");
    }
}