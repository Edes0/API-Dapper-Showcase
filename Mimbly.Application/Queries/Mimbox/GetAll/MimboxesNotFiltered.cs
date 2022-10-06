namespace Mimbly.Application.Queries.Mimbly;

using System.Collections.Generic;
using global::Mimbly.Application.Contracts.Dtos;

public class MimboxesNotFiltered
{
    public IEnumerable<MimboxDto> Mimboxes { get; set; }

    public MimboxesNotFiltered() => Mimboxes = new List<MimboxDto>();
}