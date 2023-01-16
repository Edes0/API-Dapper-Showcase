namespace Mimbly.Application.Queries.MimboxLog.GetById;

using MediatR;

public record GetByIdMimboxLogQuery : IRequest<MimboxLogByIdVm>
{
    public Guid Id { get; set; }
}