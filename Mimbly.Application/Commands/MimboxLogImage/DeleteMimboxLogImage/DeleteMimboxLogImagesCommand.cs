namespace Mimbly.Application.Commands.MimboxLogImage.DeleteMimboxLogImage;

using MediatR;

public class DeleteMimboxLogImageCommand : IRequest
{
    public Guid Id { get; init; }
}