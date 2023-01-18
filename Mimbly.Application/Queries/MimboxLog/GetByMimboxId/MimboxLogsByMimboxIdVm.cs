namespace Mimbly.Application.Queries.MimboxLog.GetByMimboxId;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.MimboxLog;

public class MimboxLogsByMimboxIdVm
{
    public IEnumerable<MimboxLogDto> MimboxLogs { get; set; }

    public MimboxLogsByMimboxIdVm() => MimboxLogs = new List<MimboxLogDto>();
}