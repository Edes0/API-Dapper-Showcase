namespace Mimbly.Application.Queries.MimboxStatus.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;

public class GetAllMimboxStatusesHandler : IRequestHandler<GetAllMimboxStatusesQuery, AllMimboxStatusesVm>
{
    private readonly IMimboxStatusRepository _mimboxStatusRepository;
    private readonly IMapper _mapper;

    public GetAllMimboxStatusesHandler(
        IMimboxStatusRepository mimboxStatusRepository,
        IMapper mapper)
    {
        _mimboxStatusRepository = mimboxStatusRepository;
        _mapper = mapper;
    }

    public async Task<AllMimboxStatusesVm> Handle(GetAllMimboxStatusesQuery request, CancellationToken cancellationToken)
    {
        var mimboxStatuses = await _mimboxStatusRepository.GetAllMimboxStatuses();

        var mimboxStatusDtos = _mapper.Map<IEnumerable<MimboxStatusDto>>(mimboxStatuses);

        return new AllMimboxStatusesVm { MimboxStatuses = mimboxStatusDtos };
    }
}