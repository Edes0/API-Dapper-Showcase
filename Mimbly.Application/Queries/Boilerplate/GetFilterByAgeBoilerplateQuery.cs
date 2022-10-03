namespace Mimbly.Application.Queries.Mimbly;

using MediatR;

public class GetMimblyByMinAgeQuery : IRequest<MimblysFilteredByAgeVm>
{
    public int Age { get; set; }
}