namespace Mimbly.Application.Queries.Mimbox.GetById;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using global::Mimbly.Application.Common.Interfaces;
using global::Mimbly.Application.Contracts.Dtos.Mimbox;
using global::Mimbly.Application.Queries.Mimbox.GetByAge;
using MediatR;

public class GetFilterByIdMimboxHandler : IRequestHandler<GetFilterByIdMimboxQuery, MimboxFilteredById>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetFilterByIdMimboxHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<MimboxFilteredById> Handle(GetFilterByIdMimboxQuery request, CancellationToken cancellationToken)
    {
        var mimbox = await _mimboxRepository.GetMimboxById(request.Id);

        var mimboxDto = _mapper.Map<MimboxDto>(mimbox);

        return new MimboxFilteredById
        {
            Mimbox = mimboxDto
        };
    }
}