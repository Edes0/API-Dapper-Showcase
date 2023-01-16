namespace Mimbly.Application.Queries.MimboxLogImage.GetById;

using MediatR;

public record GetByIdMimboxLogImageQuery : IRequest<MimboxLogImageByIdVm>
{
    public Guid Id { get; set; }
}