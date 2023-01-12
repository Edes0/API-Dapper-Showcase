namespace Mimbly.Application.Commands.MimboxLog.DeleteMimboxLog;

using AutoMapper;
using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;

public class DeleteMimboxLogCommandHandler : IRequestHandler<DeleteMimboxLogCommand>
{
    private readonly IMimboxLogRepository _mimboxLogRepository;

    public DeleteMimboxLogCommandHandler(
        IMimboxLogRepository mimboxLogRepository)
    {
        _mimboxLogRepository = mimboxLogRepository;
    }

    public async Task<Unit> Handle(DeleteMimboxLogCommand request, CancellationToken cancellationToken)
    {
        var mimboxLog = await _mimboxLogRepository.GetMimboxLogById(request.Id);

        if (mimboxLog == null)
            throw new NotFoundException($"Can't find mimbox log with id: {request.Id}");

        await _mimboxLogRepository.DeleteMimboxLog(mimboxLog);

        return Unit.Value;
    }
}