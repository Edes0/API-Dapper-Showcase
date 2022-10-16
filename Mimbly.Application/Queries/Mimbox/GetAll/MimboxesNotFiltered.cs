namespace Mimbly.Application.Queries.Mimbox.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class MimboxesNotFiltered
{
    public IEnumerable<MimboxDto> Mimboxes { get; set; }

    public MimboxesNotFiltered() => Mimboxes = new List<MimboxDto>();
}