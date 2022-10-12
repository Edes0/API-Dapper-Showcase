namespace Mimbly.Application.Queries.Mimbox.GetByAge;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using global::Mimbly.Application.Common.Interfaces;
using global::Mimbly.Application.Contracts.Dtos.Mimbox;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Mimbly.CoreServices.Exceptions;
using Mimbly.Domain.Enitites;

public class GetFilterByAgeMimboxHandler : IRequestHandler<GetFilterByAgeMimboxQuery, MimboxesFilteredByAge>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetFilterByAgeMimboxHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<MimboxesFilteredByAge> Handle(GetFilterByAgeMimboxQuery request, CancellationToken cancellationToken)
    {
        var mimboxes = await _mimboxRepository.GetMimboxesFilteredMinByAge(request.Age);

        if (mimboxes.IsNullOrEmpty()) throw new NotFoundException($"Can't find mimbox with age: {request.Age} or above");

        var mimboxDtos = _mapper.Map<IEnumerable<MimboxDto>>(mimboxes);

        return new MimboxesFilteredByAge
        {
            Mimboxes = mimboxDtos
        };
    }
}