namespace Mimbly.Application.Queries.Mimbox.GetByAge;

using MediatR;

public record GetFilterByAgeMimboxQuery : IRequest<MimboxesFilteredByAge>
{
    public int Age { get; set; }
}