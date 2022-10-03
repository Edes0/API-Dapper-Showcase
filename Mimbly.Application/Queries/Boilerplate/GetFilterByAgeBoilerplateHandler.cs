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
    private readonly IMimblyRepository _MimblyRepository;
    private readonly IMapper _mapper;

    public GetMimblyByMinAgeHandler(
        IMimblyRepository MimblyRepository,
        IMapper mapper)
    {
        _MimblyRepository = MimblyRepository;
        _mapper = mapper;
    }

    public async Task<MimblysFilteredByAgeVm> Handle(GetMimblyByMinAgeQuery request, CancellationToken cancellationToken)
    {
        var Mimblys = await _MimblyRepository.GetMimblysFilteredMinByAge(request.Age);

        var MimblyDtos = _mapper.Map<IEnumerable<MimblyDto>>(Mimblys);

        return new MimblysFilteredByAgeVm
        {
            Mimblys = MimblyDtos
        };
    }
}