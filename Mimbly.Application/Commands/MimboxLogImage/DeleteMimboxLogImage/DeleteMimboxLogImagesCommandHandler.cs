namespace Mimbly.Application.Commands.MimboxLogImage.DeleteMimboxLogImage;

using Common.Interfaces;
using MediatR;
using Mimbly.CoreServices.Exceptions;

public class DeleteMimboxLogImageCommandHandler : IRequestHandler<DeleteMimboxLogImageCommand>
{
    private readonly IMimboxLogImageRepository _mimboxLogImageRepository;

    public DeleteMimboxLogImageCommandHandler(
        IMimboxLogImageRepository mimboxLogImageRepository)
    {
        _mimboxLogImageRepository = mimboxLogImageRepository;
    }

    public async Task<Unit> Handle(DeleteMimboxLogImageCommand request, CancellationToken cancellationToken)
    {
        var mimboxLogImage = await _mimboxLogImageRepository.GetMimboxLogImageById(request.Id);

        if (mimboxLogImage == null)
            throw new NotFoundException($"Can't find mimbox log image with id: {request.Id}");

        await _mimboxLogImageRepository.DeleteMimboxLogImage(mimboxLogImage);

        return Unit.Value;
    }
}