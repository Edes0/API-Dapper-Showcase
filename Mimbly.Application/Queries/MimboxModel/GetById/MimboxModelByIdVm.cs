namespace Mimbly.Application.Queries.MimboxModel.GetById;

using Mimbly.Application.Contracts.Dtos.MimboxModel;

public class MimboxModelByIdVm
{
    public MimboxModelDto MimboxModel { get; set; }

    public MimboxModelByIdVm() => MimboxModel = new MimboxModelDto();
}
