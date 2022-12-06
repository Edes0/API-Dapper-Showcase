namespace Mimbly.Api.Controllers.v1;

using Mimbly.Application.Commands.MimboxErrorLog.UpdateMimboxErrorLog;
using Mimbly.Application.Contracts.Dtos.MimboxErrorLog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
//[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MimboxErrorLogController : BaseController
{
    public MimboxErrorLogController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateMimboxErrorLogDiscarded(Guid id, [FromBody] UpdateMimboxErrorLogRequestDto updateMimboxErrorLogRequestDto)
    {
        await _mediator.Send(new UpdateMimboxErrorLogCommand { UpdateMimboxErrorLogRequest = updateMimboxErrorLogRequestDto, Id = id });

        return Ok("Mimbox error log updated successfully");
    }
}