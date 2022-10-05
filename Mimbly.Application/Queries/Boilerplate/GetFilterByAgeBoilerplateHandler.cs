namespace Mimbly.Application.Queries.Mimbly;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using global::Mimbly.Application.Common.Interfaces;
using global::Mimbly.Application.Contracts.Dtos;
using MediatR;

public class GetMimblyByMinAgeHandler : IRequestHandler<GetMimblyByMinAgeQuery, MimblysFilteredByAgeVm>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetMimblyByMinAgeHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<MimblysFilteredByAgeVm> Handle(GetMimblyByMinAgeQuery request, CancellationToken cancellationToken)
    {
        var Mimblys = await _mimboxRepository.GetMimblysFilteredMinByAge(request.Age);

        var MimblyDtos = _mapper.Map<IEnumerable<MimblyDto>>(Mimblys);

        return new MimblysFilteredByAgeVm
        {
            Mimblys = MimblyDtos
        };
    }
}