namespace Mimbly.Application.Queries.MimboxLocation.GetById;

using MediatR;

public record GetByIdMimboxLocationQuery : IRequest<MimboxLocationByIdVm>
{
    public Guid Id { get; set; }
}