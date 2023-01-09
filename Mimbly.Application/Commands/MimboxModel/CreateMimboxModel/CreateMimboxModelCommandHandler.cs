namespace Mimbly.Application.Commands.MimboxModel.CreateMimboxModel;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateMimboxModelCommandHandler : IRequestHandler<CreateMimboxModelCommand, MimboxModel>
{
    private readonly IMimboxModelRepository _mimboxModelRepository;
    private readonly IMapper _mapper;

    public CreateMimboxModelCommandHandler(
        IMimboxModelRepository mimboxModelRepository,
        IMapper mapper)
    {
        _mimboxModelRepository = mimboxModelRepository;
        _mapper = mapper;
    }

    public async Task<MimboxModel> Handle(CreateMimboxModelCommand request, CancellationToken cancellationToken)
    {
        await request.CreateMimboxModelRequest.Validate();

        var mimboxModelEntity = _mapper.Map<MimboxModel>(request.CreateMimboxModelRequest);

        await _mimboxModelRepository.CreateMimboxModel(mimboxModelEntity);

        return mimboxModelEntity;
    }
}
