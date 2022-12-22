namespace Mimbly.Application.Queries.MimboxModel.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.MimboxModel;

public class AllMimboxModelsVm
{
    public IEnumerable<MimboxModelDto> MimboxModels { get; set; }

    public AllMimboxModelsVm() => MimboxModels = new List<MimboxModelDto>();
}