namespace Mimbly.Application.Queries.MimboxLog.GetByMimboxId;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxLog;
using Mimbly.CoreServices.Exceptions;

public class GetByMimboxIdMimboxLogHandler : IRequestHandler<GetByMimboxIdMimboxLogQuery, MimboxLogsByMimboxIdVm>
{
    private readonly IMimboxLogRepository _mimboxLogRepository;
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetByMimboxIdMimboxLogHandler(
        IMimboxLogRepository mimboxLogRepository,
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxLogRepository = mimboxLogRepository;
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLogsByMimboxIdVm> Handle(GetByMimboxIdMimboxLogQuery request, CancellationToken cancellationToken)
    {
        var mimbox = await _mimboxRepository.GetMimboxById(request.Id);

        if (mimbox == null)
            throw new NotFoundException($"Can't find mimbox with id: {request.Id}");

        var mimboxLogs = await _mimboxLogRepository.GetMimboxLogsByMimboxId(mimbox.Id);

        var mimboxLogDtos = _mapper.Map<IEnumerable<MimboxLogDto>>(mimboxLogs);

        return new MimboxLogsByMimboxIdVm { MimboxLogs = mimboxLogDtos };
    }
}
