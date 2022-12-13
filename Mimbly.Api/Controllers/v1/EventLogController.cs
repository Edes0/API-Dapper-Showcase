namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Queries.EventLog.GetByMimboxId;

[ApiController]
//[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class EventLogController : BaseController
{
    public EventLogController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:guid}", Name = "EventLogByMimboxId")]
    public async Task<ActionResult<EventLogByMimboxIdVm>> FilterEventLogsById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByMimboxIdEventLogQuery { Id = id }));
    }
}