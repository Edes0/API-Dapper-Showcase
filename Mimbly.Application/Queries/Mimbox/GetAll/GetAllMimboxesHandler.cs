namespace Mimbly.Application.Queries.Mimbox.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using global::Mimbly.Application.Common.Interfaces;
using global::Mimbly.Application.Contracts.Dtos;
using global::Mimbly.Application.Queries.Mimbly;
using MediatR;

public class GetAllMimboxesHandler : IRequestHandler<GetAllMimboxesQuery, MimboxesNotFiltered>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetAllMimboxesHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<MimboxesNotFiltered> Handle(GetAllMimboxesQuery request, CancellationToken cancellationToken)
    {
        var mimboxes = await _mimboxRepository.GetAllMimboxes();

        var mimboxDtos = _mapper.Map<IEnumerable<MimboxDto>>(mimboxes);

        return new MimboxesNotFiltered
        {
            Mimboxes = mimboxDtos
        };
    }
}