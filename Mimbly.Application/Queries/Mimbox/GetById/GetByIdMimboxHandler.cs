namespace Mimbly.Application.Queries.Mimbox.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.CoreServices.Exceptions;
using Microsoft.IdentityModel.Tokens;

public class GetByIdMimboxHandler : IRequestHandler<GetByIdMimboxQuery, MimboxByIdVm>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public GetByIdMimboxHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<MimboxByIdVm> Handle(GetByIdMimboxQuery request, CancellationToken cancellationToken)
    {
        var mimbox = await _mimboxRepository.GetMimboxById(request.Id);

        if (mimbox.IsNullOrEmpty())
            throw new NotFoundException($"Can't find mimbox with id: {request.Id}");

        var mimboxDto = _mapper.Map<MimboxDto>(mimbox.First());

        return new MimboxByIdVm { Mimbox = mimboxDto };
    }
}