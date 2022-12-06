namespace Mimbly.Application.Queries.MimboxLocation.GetById;

using MediatR;
using Mimbly.Application.Queries.Mimbox.GetById;

public record GetByIdMimboxLocationQuery : IRequest<MimboxLocationByIdVm>
{
    public Guid Id { get; set; }
}