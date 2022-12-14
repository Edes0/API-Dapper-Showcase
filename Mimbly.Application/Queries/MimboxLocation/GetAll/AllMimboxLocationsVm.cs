namespace Mimbly.Application.Queries.MimboxLocation.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.MimboxLocation;

public class AllMimboxLocationsVm
{
    public IEnumerable<MimboxLocationDto> MimboxLocations { get; set; }

    public AllMimboxLocationsVm() => MimboxLocations = new List<MimboxLocationDto>();
}