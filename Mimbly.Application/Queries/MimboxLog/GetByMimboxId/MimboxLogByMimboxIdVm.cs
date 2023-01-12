namespace Mimbly.Application.Queries.MimboxLog.GetByMimboxId;

using Mimbly.Application.Contracts.Dtos.MimboxLocation;

public class MimboxLogByMimboxId
{
    public MimboxLocationDto MimboxLocation { get; set; }

    public MimboxLogByMimboxId() => MimboxLocation = new MimboxLocationDto();
}