namespace Mimbly.Application.Queries.Mimbox.GetAll;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.CoreServices.Exceptions;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Microsoft.IdentityModel.Tokens;
using Mimbly.Domain.Entities;

public class GetAllMimboxesHandler : IRequestHandler<GetAllMimboxesQuery, AllMimboxesVm>
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

    public async Task<AllMimboxesVm> Handle(GetAllMimboxesQuery request, CancellationToken cancellationToken)
    {
        var mimboxes = await _mimboxRepository.GetAllMimboxes();

        var mimboxDtos = _mapper.Map<IEnumerable<MimboxDto>>(mimboxes);

        return new AllMimboxesVm { Mimboxes = mimboxDtos };
    }
}