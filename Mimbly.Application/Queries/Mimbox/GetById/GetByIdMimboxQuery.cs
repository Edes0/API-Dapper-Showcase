namespace Mimbly.Application.Queries.Mimbox.GetById;

using MediatR;

public record GetByIdMimboxQuery : IRequest<MimboxByIdVm>
{
    public Guid Id { get; set; }
}