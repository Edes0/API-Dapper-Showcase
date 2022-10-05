namespace Mimbly.Application.Queries.Mimbly;

using MediatR;

public record GetMimblyByMinAgeQuery : IRequest<MimblysFilteredByAgeVm>
{
    public int Age { get; set; }
}