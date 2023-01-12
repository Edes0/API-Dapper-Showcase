namespace Mimbly.Application.Commands.MimboxLogImageImage.CreateMimboxLogImageImage;

using MediatR;
using Mimbly.Application.Contracts.Dtos.MimboxLogImage;
using Mimbly.Domain.Entities;

public class CreateMimboxLogImageCommand : IRequest<MimboxLogImage>
{
    public CreateMimboxLogImageRequestDto CreateMimboxLogImageRequest { get; set; } = null!;
}