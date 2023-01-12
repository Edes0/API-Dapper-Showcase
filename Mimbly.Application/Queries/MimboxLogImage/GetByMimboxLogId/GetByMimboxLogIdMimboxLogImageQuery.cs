namespace Mimbly.Application.Queries.MimboxLogImage.GetByMimboxLogId;

using MediatR;

public record GetByMimboxLogIdMimboxLogImageQuery : IRequest<MimboxLogImagesByMimboxLogIdVm>
{
    public Guid Id { get; set; }
}
