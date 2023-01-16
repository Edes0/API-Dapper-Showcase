namespace Mimbly.Application.Queries.MimboxLog.GetByMimboxId;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxLog;
using Mimbly.CoreServices.Exceptions;
using Mimbly.Domain.Entities;

public class GetByMimboxIdMimboxLogHandler : IRequestHandler<GetByMimboxIdMimboxLogQuery, MimboxLogsByMimboxIdVm>
{
    private readonly IMimboxLogRepository _mimboxLogRepository;
    private readonly IMimboxLogImageRepository _mimboxLogImageRepository;
    private readonly IMapper _mapper;

    public GetByMimboxIdMimboxLogHandler(
        IMimboxLogRepository mimboxLogRepository,
        IMimboxLogImageRepository mimboxLogImageRepository,
        IMapper mapper)
    {
        _mimboxLogRepository = mimboxLogRepository;
        _mimboxLogImageRepository = mimboxLogImageRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLogsByMimboxIdVm> Handle(GetByMimboxIdMimboxLogQuery request, CancellationToken cancellationToken)
    {
        var mimboxLogs = await _mimboxLogRepository.GetMimboxLogsByMimboxId(request.Id);

        if (mimboxLogs != null)
        {
            var mimboxLogIds = mimboxLogs.Select(x => x.Id);
            var mimboxLogImages = await _mimboxLogImageRepository.GetMimboxLogImagesByMimboxLogIds(mimboxLogIds);

            foreach (var log in mimboxLogs)
            {
                var currentMimboxLogImages = mimboxLogImages.Where(x => x.MimboxLogId == log.Id).Select(x => x);

                log.ImageList = currentMimboxLogImages.ToList();
            }

            var mimboxLogDtos = _mapper.Map<IEnumerable<MimboxLogDto>>(mimboxLogs);

            return new MimboxLogsByMimboxIdVm { MimboxLogs = mimboxLogDtos };
        }
        return null;
    }
}
