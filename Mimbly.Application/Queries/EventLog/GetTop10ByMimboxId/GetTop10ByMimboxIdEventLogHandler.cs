namespace Mimbly.Application.Queries.EventLog.GetTop10ByMimboxId;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.CoreServices.Exceptions;

public class GetTop10ByMimboxIdEventLogHandler : IRequestHandler<GetTop10ByMimboxIdEventLogQuery, EventLogTop10ByMimboxIdVm>
{
    private readonly IEventLogRepository _eventLogRepository;

    public GetTop10ByMimboxIdEventLogHandler(
        IEventLogRepository eventLogRepository)
    {
        _eventLogRepository = eventLogRepository;
    }

    public async Task<EventLogTop10ByMimboxIdVm> Handle(GetTop10ByMimboxIdEventLogQuery request, CancellationToken cancellationToken)
    {
        var logs = await _eventLogRepository.GetTop10EventLogByMimboxId(request.Id);

        if (logs == null)
            throw new NotFoundException($"Can't find event logs for mimbox with id: {request.Id}");

        return new EventLogTop10ByMimboxIdVm { Log = logs };
    }
}