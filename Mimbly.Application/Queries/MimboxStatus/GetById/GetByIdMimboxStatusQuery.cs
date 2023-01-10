namespace Mimbly.Application.Queries.MimboxStatus.GetById;

using MediatR;

public record GetByIdMimboxStatusQuery : IRequest<MimboxStatusByIdVm>
{
    public Guid Id { get; set; }
}