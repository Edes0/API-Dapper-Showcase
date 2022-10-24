namespace Mimbly.Application.Queries.Mimbox.GetById;

using Mimbly.Application.Contracts.Dtos.Mimbox;

public class MimboxByIdVm
{
    public MimboxDto Mimbox { get; set; }

    public MimboxByIdVm() => Mimbox = new MimboxDto();
}