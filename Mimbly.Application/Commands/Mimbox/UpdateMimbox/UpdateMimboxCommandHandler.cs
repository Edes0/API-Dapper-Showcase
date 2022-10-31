namespace Mimbly.Application.Commands.Mimbox.UpdateMimbox;

using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class UpdateMimblyCommandHandler : IRequestHandler<UpdateMimboxCommand>
{
    private readonly IMimboxRepository _mimboxRepository;
    private readonly IMapper _mapper;

    public UpdateMimblyCommandHandler(
        IMimboxRepository mimboxRepository,
        IMapper mapper)
    {
        _mimboxRepository = mimboxRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMimboxCommand request, CancellationToken cancellationToken)
    {
        await request.UpdateMimboxRequest.Validate();

        var mimboxEntity = _mapper.Map<Mimbox>(request.UpdateMimboxRequest);

        await _mimboxRepository.UpdateMimbox(mimboxEntity);

        // This runs a single task. If several entities use Task.WhenAll
        await Task.Run(() => request.UpdateMimboxRequest.Validate(), cancellationToken);

        return Unit.Value;
    }
}
