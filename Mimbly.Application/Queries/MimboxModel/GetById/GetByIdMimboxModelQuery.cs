namespace Mimbly.Application.Queries.MimboxModel.GetById;

using MediatR;

public record GetByIdMimboxModelQuery : IRequest<MimboxModelByIdVm>
{
    public Guid Id { get; set; }
}