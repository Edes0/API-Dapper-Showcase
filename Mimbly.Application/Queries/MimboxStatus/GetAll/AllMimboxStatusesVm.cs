namespace Mimbly.Application.Queries.MimboxStatus.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.MimboxStatus;

public class AllMimboxStatusesVm
{
    public IEnumerable<MimboxStatusDto> MimboxStatuses { get; set; }

    public AllMimboxStatusesVm() => MimboxStatuses = new List<MimboxStatusDto>();
}
