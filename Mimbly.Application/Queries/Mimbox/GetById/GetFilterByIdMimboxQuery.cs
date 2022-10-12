namespace Mimbly.Application.Queries.Mimbox.GetById;

using MediatR;

public record GetFilterByIdMimboxQuery : IRequest<MimboxFilteredById>
{
    public Guid Id { get; set; }
}