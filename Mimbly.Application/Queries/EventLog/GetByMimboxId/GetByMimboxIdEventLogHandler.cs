namespace Mimbly.Application.Queries.EventLog.GetByMimboxId;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.CoreServices.Exceptions;

public class GetByMimboxIdEventLogHandler : IRequestHandler<GetByMimboxIdEventLogQuery, EventLogByMimboxIdVm>
{
    private readonly IEventLogRepository _eventLogRepository;

    public GetByMimboxIdEventLogHandler(
        IEventLogRepository eventLogRepository)
    {
        _eventLogRepository = eventLogRepository;
    }

    public async Task<EventLogByMimboxIdVm> Handle(GetByMimboxIdEventLogQuery request, CancellationToken cancellationToken)
    {
        var logs = await _eventLogRepository.GetEventLogByMimboxId(request.Id);

        if (logs == null)
            throw new NotFoundException($"Can't find event logs for mimbox with id: {request.Id}");

        return new EventLogByMimboxIdVm { Log = logs };
    }
}