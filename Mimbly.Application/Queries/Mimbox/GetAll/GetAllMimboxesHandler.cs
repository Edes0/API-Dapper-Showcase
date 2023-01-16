namespace Mimbly.Application.Queries.Mimbox.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class GetAllMimboxesHandler : IRequestHandler<GetAllMimboxesQuery, AllMimboxesVm>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMimboxErrorLogRepository _mimboxErrorLogRepository;
    private readonly IMapper _mapper;

    public GetAllMimboxesHandler(
        IMimboxRepository mimboxRepository,
        IMimboxErrorLogRepository mimboxErrorLogRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mimboxErrorLogRepository = mimboxErrorLogRepository;
        _mapper = mapper;
    }

    public async Task<AllMimboxesVm> Handle(GetAllMimboxesQuery request, CancellationToken cancellationToken)
    {
        var mimboxes = await _mimboxRepository.GetAllMimboxes();
        var mimboxIds = mimboxes.Select(x => x.Id);
        var errorLogs = await _mimboxErrorLogRepository.GetErrorLogsByMimboxIds(mimboxIds);

        foreach (var mimbox in mimboxes)
        {
            var currentMimboxErrorLogList = errorLogs.Where(x => x.MimboxId == mimbox.Id).Select(x => x);
            mimbox.ErrorLogList = currentMimboxErrorLogList.ToList();
        }

        var mimboxDtos = _mapper.Map<IEnumerable<MimboxDto>>(mimboxes);

        return new AllMimboxesVm { Mimboxes = mimboxDtos };
    }
}