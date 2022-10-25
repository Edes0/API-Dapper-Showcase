namespace Mimbly.Application.Queries.Mimbox.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.Mimbox;

public class AllMimboxesVm
{
    public IEnumerable<MimboxDto> Mimboxes { get; set; }

    public AllMimboxesVm() => Mimboxes = new List<MimboxDto>();
}