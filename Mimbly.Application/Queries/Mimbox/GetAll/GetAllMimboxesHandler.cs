namespace Mimbly.Application.Queries.Mimbox.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.CoreServices.Exceptions;
using global::Mimbly.Application.Common.Interfaces;
using global::Mimbly.Application.Contracts.Dtos.Mimbox;
using Microsoft.IdentityModel.Tokens;

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

        if (mimboxes.IsNullOrEmpty()) throw new NotFoundException("No mimboxes found in database");

        var mimboxDtos = _mapper.Map<IEnumerable<MimboxDto>>(mimboxes);

        return new MimboxesNotFiltered
        {
            Mimboxes = mimboxDtos
        };
    }
}