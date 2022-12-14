namespace Mimbly.Application.Queries.EventLog.GetBetweenDatesByMimboxId;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.CoreServices.Exceptions;

public class GetBetweenDatesByMimboxIdEventLogHandler : IRequestHandler<GetBetweenDatesByMimboxIdEventLogQuery, EventLogBetweenDatesByMimboxIdVm>
{
    private readonly IEventLogRepository _eventLogRepository;

    public GetBetweenDatesByMimboxIdEventLogHandler(
        IEventLogRepository eventLogRepository)
    {
        _eventLogRepository = eventLogRepository;
    }

    public async Task<EventLogBetweenDatesByMimboxIdVm> Handle(GetBetweenDatesByMimboxIdEventLogQuery request, CancellationToken cancellationToken)
    {
        var logs = await _eventLogRepository.GetEventLogBetweenDatesByMimboxId(request.Id, request.StartDate, request.EndDate);

        if (logs == null)
            throw new NotFoundException($"Can't find event logs between dates {request.StartDate} and {request.EndDate} for mimbox with id: {request.Id}");

        return new EventLogBetweenDatesByMimboxIdVm { Log = logs };
    }
}