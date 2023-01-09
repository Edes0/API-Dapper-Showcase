namespace Mimbly.Application.Queries.MimboxStatus.GetById;

using Mimbly.Application.Contracts.Dtos.MimboxStatus;

public class MimboxStatusByIdVm
{
    public MimboxStatusDto MimboxStatus { get; set; }

    public MimboxStatusByIdVm() => MimboxStatus = new MimboxStatusDto();
}