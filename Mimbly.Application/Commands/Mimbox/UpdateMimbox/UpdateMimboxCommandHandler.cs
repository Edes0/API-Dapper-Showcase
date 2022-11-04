namespace Mimbly.Application.Commands.Mimbox.UpdateMimbox;

using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class UpdateMimboxCommandHandler : IRequestHandler<UpdateMimboxCommand>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public UpdateMimboxCommandHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMimboxCommand request)
    {
        await request.UpdateMimboxRequest.Validate();

        var mimboxEntity = _mapper.Map<Mimbox>(request.UpdateMimboxRequest);

        mimboxEntity.Id = request.Id;

        await _mimboxRepository.UpdateMimbox(mimboxEntity);

        return Unit.Value;
    }
}
