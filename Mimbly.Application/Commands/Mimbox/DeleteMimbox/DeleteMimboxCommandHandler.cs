namespace Mimbly.Application.Commands.Mimbox.DeleteMimbox;

using Common.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Mimbly.CoreServices.Exceptions;

public class DeleteMimblyCommandHandler : IRequestHandler<DeleteMimboxCommand>
{
    private readonly IMimboxRepository _mimboxRepository;

    public DeleteMimblyCommandHandler(IMimboxRepository mimboxRepository)
    {
        _mimboxRepository = mimboxRepository;
    }

    public async Task<Unit> Handle(DeleteMimboxCommand request, CancellationToken cancellationToken)
    {
        var mimbox = await _mimboxRepository.GetMimboxById(request.Id);

        if (mimbox.IsNullOrEmpty()) throw new NotFoundException($"Can't find mimbox with id: {request.Id}");

        await _mimboxRepository.DeleteMimbox(mimbox.First()); //TODO: Snygga till?

        return Unit.Value;
    }
}