namespace Mimbly.Application.Commands.MimboxStatus.UpdateMimboxStatus;

using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class UpdateMimboxStatusCommandHandler : IRequestHandler<UpdateMimboxStatusCommand>
{
    private readonly IMimboxStatusRepository _mimboxStatusRepository;
    private readonly IMapper _mapper;

    public UpdateMimboxStatusCommandHandler(
        IMimboxStatusRepository mimboxStatusRepository,
        IMapper mapper)
    {
        _mimboxStatusRepository = mimboxStatusRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMimboxStatusCommand request, CancellationToken cancellationToken)
    {
        await request.UpdateMimboxStatusRequest.Validate();

        var mimboxStatusEntity = _mapper.Map<MimboxStatus>(request.UpdateMimboxStatusRequest);

        mimboxStatusEntity.Id = request.Id;

        await _mimboxStatusRepository.UpdateMimboxStatus(mimboxStatusEntity);

        return Unit.Value;
    }
}