namespace Mimbly.Application.Commands.MimboxModel.UpdateMimboxModel;

using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.CoreServices.Exceptions;
using Mimbly.Domain.Entities;

public class UpdateMimboxModelCommandHandler : IRequestHandler<UpdateMimboxModelCommand>
{
    private readonly IMimboxModelRepository _mimboxModelRepository;
    private readonly IMapper _mapper;

    public UpdateMimboxModelCommandHandler(
        IMimboxModelRepository mimboxModelRepository,
        IMapper mapper)
    {
        _mimboxModelRepository = mimboxModelRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMimboxModelCommand request, CancellationToken cancellationToken)
    {
        var mimboxModelEntity = _mapper.Map<MimboxModel>(request.UpdateMimboxModelRequest);

        mimboxModelEntity.Id = request.Id;

        await _mimboxModelRepository.UpdateMimboxModel(mimboxModelEntity);

        return Unit.Value;
    }
}