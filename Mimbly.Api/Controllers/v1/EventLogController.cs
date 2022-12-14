namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Queries.EventLog.GetBetweenDatesByMimboxId;
using Mimbly.Application.Queries.EventLog.GetByMimboxId;
using Mimbly.Application.Queries.EventLog.GetTop10ByMimboxId;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class EventLogController : BaseController
{
    public EventLogController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:guid}", Name = "EventLogByMimboxId")]
    public async Task<ActionResult<EventLogByMimboxIdVm>> FilterEventLogByMimboxId([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByMimboxIdEventLogQuery { Id = id }));
    }

    [HttpGet("Top10/{id:guid}", Name = "EventLogTop10ByMimboxId")]
    public async Task<ActionResult<EventLogTop10ByMimboxIdVm>> FilterEventLogTop10ByMimboxId([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetTop10ByMimboxIdEventLogQuery { Id = id }));
    }

    [HttpGet("BetweenDates/{id:guid}", Name = "EventLogBetweenDatesByMimboxId")]
    public async Task<ActionResult<EventLogBetweenDatesByMimboxIdVm>> FilterEventLogBetweenDatesByMimboxId([BindRequired] Guid id, DateTime startDate, DateTime endDate)
    {
        return Ok(await _mediator.Send(new GetBetweenDatesByMimboxIdEventLogQuery { Id = id, StartDate = startDate, EndDate = endDate }));
    }
}