namespace Mimbly.Application.Queries.MimboxLog.GetByMimboxId;

using MediatR;

public record GetByMimboxIdMimboxLogQuery : IRequest<MimboxLogsByMimboxIdVm>
{
    public Guid Id { get; set; }
}