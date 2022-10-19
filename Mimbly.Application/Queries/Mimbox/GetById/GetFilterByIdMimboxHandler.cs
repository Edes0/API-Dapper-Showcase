namespace Mimbly.Application.Queries.Mimbox.GetById;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.CoreServices.Exceptions;
using Microsoft.IdentityModel.Tokens;

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

        if (mimbox.IsNullOrEmpty()) throw new NotFoundException($"Can't find mimbox with id: {request.Id}");

        var mimboxDto = _mapper.Map<MimboxDto>(mimbox.First());

        return new MimboxFilteredById
        {
            Mimbox = mimboxDto
        };
    }
}