namespace Mimbly.Application.Queries.MimboxLog.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxLog;
using Mimbly.CoreServices.Exceptions;

public class GetByIdMimboxLogHandler : IRequestHandler<GetByIdMimboxLogQuery, MimboxLogByIdVm>
{
    private readonly IMimboxLogRepository _mimboxLogRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxLogHandler(
        IMimboxLogRepository mimboxLogRepository,
        IMapper mapper)
    {
        _mimboxLogRepository = mimboxLogRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLogByIdVm> Handle(GetByIdMimboxLogQuery request, CancellationToken cancellationToken)
    {
        var mimboxLog = await _mimboxLogRepository.GetMimboxLogById(request.Id);

        if (mimboxLog == null)
            throw new NotFoundException($"Can't find mimbox log with id: {request.Id}");

        var mimboxLogDto = _mapper.Map<MimboxLogDto>(mimboxLog);

        return new MimboxLogByIdVm { MimboxLog = mimboxLogDto };
    }
}