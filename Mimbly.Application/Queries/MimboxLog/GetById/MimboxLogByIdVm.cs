namespace Mimbly.Application.Queries.MimboxLog.GetById;

using Mimbly.Application.Contracts.Dtos.MimboxLog;

public class MimboxLogByIdVm
{
    public MimboxLogDto MimboxLog { get; set; }

    public MimboxLogByIdVm() => MimboxLog = new MimboxLogDto();
}