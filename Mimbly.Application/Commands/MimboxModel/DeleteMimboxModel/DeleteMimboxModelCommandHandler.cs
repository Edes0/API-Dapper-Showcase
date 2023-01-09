namespace Mimbly.Application.Commands.MimboxModel.DeleteMimboxModel;

using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;

public class DeleteMimboxModelCommandHandler : IRequestHandler<DeleteMimboxModelCommand>
{
    private readonly IMimboxModelRepository _mimboxModelRepository;

    public DeleteMimboxModelCommandHandler(
        IMimboxModelRepository mimboxModelRepository)
    {
        _mimboxModelRepository = mimboxModelRepository;
    }

    public async Task<Unit> Handle(DeleteMimboxModelCommand request, CancellationToken cancellationToken)
    {
        var mimboxModel = await _mimboxModelRepository.GetMimboxModelById(request.Id);

        if (mimboxModel == null)
            throw new NotFoundException($"Can't find model with id: {request.Id}");

        await _mimboxModelRepository.DeleteMimboxModel(mimboxModel);

        return Unit.Value;
    }
}