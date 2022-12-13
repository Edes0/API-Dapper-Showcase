namespace Mimbly.Application.Queries.EventLog.GetByMimboxId;

using MediatR;

public record GetByMimboxIdEventLogQuery : IRequest<EventLogByMimboxIdVm>
{
    public Guid Id { get; set; }
}
