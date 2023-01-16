namespace Mimbly.Application.Commands.MimboxLog.CreateMimboxLog;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.Domain.Entities;

public class CreateMimboxLogCommandHandler : IRequestHandler<CreateMimboxLogCommand, MimboxLog>
{
    private readonly IMimboxLogRepository _mimboxLogRepository;
    private readonly IMapper _mapper;

    public CreateMimboxLogCommandHandler(
        IMimboxLogRepository mimboxLogRepository,
        IMapper mapper)
    {
        _mimboxLogRepository = mimboxLogRepository;
        _mapper = mapper;
    }

    public async Task<MimboxLog> Handle(CreateMimboxLogCommand request, CancellationToken cancellationToken)
    {
        var mimboxLogEntity = _mapper.Map<MimboxLog>(request.CreateMimboxLogRequest);

        await _mimboxLogRepository.CreateMimboxLog(mimboxLogEntity);

        return mimboxLogEntity;
    }
}