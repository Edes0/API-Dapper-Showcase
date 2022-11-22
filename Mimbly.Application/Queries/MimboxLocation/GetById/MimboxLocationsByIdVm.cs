namespace Mimbly.Application.Queries.MimboxLocation.GetById;

using Mimbly.Application.Contracts.Dtos.MimboxLocation;

public class MimboxLocationByIdVm
{
    public MimboxLocationDto MimboxLocation { get; set; }

    public MimboxLocationByIdVm() => MimboxLocation = new MimboxLocationDto();
}