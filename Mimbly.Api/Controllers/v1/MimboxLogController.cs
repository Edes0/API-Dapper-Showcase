namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Api.Attributes;
using Mimbly.Application.Commands.MimboxLog.CreateMimboxLog;
using Mimbly.Application.Commands.MimboxLog.DeleteMimboxLog;
using Mimbly.Application.Commands.MimboxLog.UpdateMimboxLog;
using Mimbly.Application.Contracts.Dtos.MimboxLog;
using Mimbly.Application.Queries.MimboxLog.GetById;
using Mimbly.Application.Queries.MimboxLog.GetByMimboxId;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MimboxLogController : BaseController
{
    public MimboxLogController(IMediator mediator) : base(mediator)
    {
    }

    [ApiKey]
    [HttpGet("ByMimboxId/{mimboxId:guid}", Name = "MimboxLogByMimboxId")]
    public async Task<ActionResult<MimboxLogsByMimboxIdVm>> FilterMimboxLogsByMimboxId([BindRequired] Guid mimboxId)
    {
        return Ok(await _mediator.Send(new GetByMimboxIdMimboxLogQuery { Id = mimboxId }));
    }

    [Authorize]
    [HttpGet("{id:guid}", Name = "MimboxLogById")]
    public async Task<ActionResult<MimboxLogByIdVm>> FilterMimboxLogsById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdMimboxLogQuery { Id = id }));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateMimboxLog([FromBody] CreateMimboxLogRequestDto createMimboxLogRequestDto)
    {
        var createdMimboxLog = await _mediator.Send(new CreateMimboxLogCommand { CreateMimboxLogRequest = createMimboxLogRequestDto });

        return CreatedAtRoute("MimboxLogById", new { createdMimboxLog.Id }, createdMimboxLog);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMimboxLog([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteMimboxLogCommand { Id = id });

        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateMimboxLog(Guid id, [FromBody] UpdateMimboxLogRequestDto updateMimboxLogRequestDto)
    {
        await _mediator.Send(new UpdateMimboxLogCommand { UpdateMimboxLogRequest = updateMimboxLogRequestDto, Id = id });

        return Ok("Mimbox log updated successfully");
    }
}