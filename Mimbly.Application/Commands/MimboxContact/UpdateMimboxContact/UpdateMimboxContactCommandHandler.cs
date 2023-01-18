namespace Mimbly.Application.Commands.MimboxContact.UpdateMimboxContact;

using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class UpdateMimboxContactCommandHandler : IRequestHandler<UpdateMimboxContactCommand>
{
    private readonly IMimboxContactRepository _mimboxContactRepository;
    private readonly IMapper _mapper;

    public UpdateMimboxContactCommandHandler(
        IMimboxContactRepository mimboxContactRepository,
        IMapper mapper)
    {
        _mimboxContactRepository = mimboxContactRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMimboxContactCommand request, CancellationToken cancellationToken)
    {
        var mimboxContactEntity = _mapper.Map<MimboxContact>(request.UpdateMimboxContactRequest);

        mimboxContactEntity.Id = request.Id;

        await _mimboxContactRepository.UpdateMimboxContact(mimboxContactEntity);

        return Unit.Value;
    }
}
