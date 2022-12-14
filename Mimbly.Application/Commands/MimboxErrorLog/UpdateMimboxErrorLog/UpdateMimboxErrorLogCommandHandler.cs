namespace Mimbly.Application.Commands.MimboxErrorLog.UpdateMimboxErrorLog;

using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities.AzureEvents;

public class UpdateMimboxErrorLogCommandHandler : IRequestHandler<UpdateMimboxErrorLogCommand>
{
    private readonly IMimboxErrorLogRepository _mimboxErrorLogRepository;
    private readonly IMapper _mapper;

    public UpdateMimboxErrorLogCommandHandler(
        IMimboxErrorLogRepository mimboxErrorLogRepository,
        IMapper mapper)
    {
        _mimboxErrorLogRepository = mimboxErrorLogRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMimboxErrorLogCommand request, CancellationToken cancellationToken)
    {
        await request.UpdateMimboxErrorLogRequest.Validate();

        var mimboxErrorLogEntity = _mapper.Map<MimboxErrorLog>(request.UpdateMimboxErrorLogRequest);

        mimboxErrorLogEntity.Id = request.Id;

        await _mimboxErrorLogRepository.UpdateMimboxErrorLog(mimboxErrorLogEntity);

        return Unit.Value;
    }
}