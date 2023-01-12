namespace Mimbly.Application.Commands.MimboxLog.UpdateMimboxLog;

using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Domain.Entities;

public class UpdateMimboxLogCommandHandler : IRequestHandler<UpdateMimboxLogCommand>
{
    private readonly IMimboxLogRepository _mimboxLogRepository;
    private readonly IMapper _mapper;

    public UpdateMimboxLogCommandHandler(
        IMimboxLogRepository mimboxLogRepository,
        IMapper mapper)
    {
        _mimboxLogRepository = mimboxLogRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMimboxLogCommand request, CancellationToken cancellationToken)
    {
        var mimboxLogEntity = _mapper.Map<MimboxLog>(request.UpdateMimboxLogRequest);

        mimboxLogEntity.Id = request.Id;

        await _mimboxLogRepository.UpdateMimboxLog(mimboxLogEntity);

        return Unit.Value;
    }
}
