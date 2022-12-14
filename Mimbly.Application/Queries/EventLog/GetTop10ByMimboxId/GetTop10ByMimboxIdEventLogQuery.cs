namespace Mimbly.Application.Queries.EventLog.GetTop10ByMimboxId;

using MediatR;

public record GetTop10ByMimboxIdEventLogQuery : IRequest<EventLogTop10ByMimboxIdVm>
{
    public Guid Id { get; set; }
}
