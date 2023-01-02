namespace Mimbly.Application.Commands.MimboxContact.DeleteMimboxContact;

using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;

public class DeleteMimboxContactCommandHandler : IRequestHandler<DeleteMimboxContactCommand>
{
    private readonly IMimboxContactRepository _mimboxContactRepository;

    public DeleteMimboxContactCommandHandler(
        IMimboxContactRepository mimboxContactRepository)
    {
        _mimboxContactRepository = mimboxContactRepository;
    }

    public async Task<Unit> Handle(DeleteMimboxContactCommand request, CancellationToken cancellationToken)
    {
        var mimboxContact = await _mimboxContactRepository.GetMimboxContactById(request.Id);

        if (mimboxContact == null)
            throw new NotFoundException($"Can't find Mimbox contact with id: {request.Id}");

        await _mimboxContactRepository.DeleteMimboxContact(mimboxContact);

        return Unit.Value;
    }
}
