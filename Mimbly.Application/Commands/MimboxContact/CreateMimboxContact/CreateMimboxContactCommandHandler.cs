namespace Mimbly.Application.Commands.MimboxContact.CreateMimboxContact;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateMimboxCommandHandler : IRequestHandler<CreateMimboxContactCommand, MimboxContact>
{
    private readonly IMimboxContactRepository _mimboxContactRepository;
    private readonly IMapper _mapper;

    public CreateMimboxCommandHandler(
        IMimboxContactRepository mimboxContactRepository,
        IMapper mapper)
    {
        _mimboxContactRepository = mimboxContactRepository;
        _mapper = mapper;
    }

    public async Task<MimboxContact> Handle(CreateMimboxContactCommand request, CancellationToken cancellationToken)
    {
        await request.CreateMimboxContactRequest.Validate();

        var mimboxContactEntity = _mapper.Map<MimboxContact>(request.CreateMimboxContactRequest);

        await _mimboxContactRepository.CreateMimboxContact(mimboxContactEntity);

        return mimboxContactEntity;
    }
}
