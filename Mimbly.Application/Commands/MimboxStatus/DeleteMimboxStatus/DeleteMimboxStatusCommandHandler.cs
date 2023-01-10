namespace Mimbly.Application.Commands.MimboxStatus.DeleteMimboxStatus;

using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;

public class DeleteMimboxStatusCommandHandler : IRequestHandler<DeleteMimboxStatusCommand>
{
    private readonly IMimboxStatusRepository _mimboxStatusRepository;

    public DeleteMimboxStatusCommandHandler(
        IMimboxStatusRepository mimboxStatusRepository)
    {
        _mimboxStatusRepository = mimboxStatusRepository;
    }

    public async Task<Unit> Handle(DeleteMimboxStatusCommand request, CancellationToken cancellationToken)
    {
        var mimboxStatus = await _mimboxStatusRepository.GetMimboxStatusById(request.Id);

        if (mimboxStatus == null)
            throw new NotFoundException($"Can't find status with id: {request.Id}");

        await _mimboxStatusRepository.DeleteMimboxStatus(mimboxStatus);

        return Unit.Value;
    }
}