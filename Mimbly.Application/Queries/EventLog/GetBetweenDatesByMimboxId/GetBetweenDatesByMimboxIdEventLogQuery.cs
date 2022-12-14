namespace Mimbly.Application.Queries.EventLog.GetBetweenDatesByMimboxId;

using MediatR;

public record GetBetweenDatesByMimboxIdEventLogQuery : IRequest<EventLogBetweenDatesByMimboxIdVm>
{
    public Guid Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
