namespace Mimbly.Application.Queries.MimboxContact.GetById;

using Mimbly.Application.Contracts.Dtos.MimboxContact;

public class MimboxContactByIdVm
{
    public MimboxContactDto MimboxContact { get; set; }

    public MimboxContactByIdVm() => MimboxContact = new MimboxContactDto();
}