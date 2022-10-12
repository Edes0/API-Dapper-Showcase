namespace Mimbly.Application.Queries.Mimbox.GetByAge;

using System.Collections.Generic;
using global::Mimbly.Application.Contracts.Dtos.Mimbox;

public class MimboxesFilteredByAge
{
    public IEnumerable<MimboxDto> Mimboxes { get; set; }

    public MimboxesFilteredByAge() => Mimboxes = new List<MimboxDto>();
}