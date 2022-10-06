namespace Mimbly.Application.Queries.Mimbly;

using System.Collections.Generic;
using global::Mimbly.Application.Contracts.Dtos;

public class MimblysFilteredByAgeVm
{
    public IEnumerable<MimboxDto> Mimblys { get; set; }

    public MimblysFilteredByAgeVm() => Mimblys = new List<MimboxDto>();
}