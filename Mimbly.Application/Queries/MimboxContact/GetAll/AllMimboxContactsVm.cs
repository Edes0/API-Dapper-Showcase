namespace Mimbly.Application.Queries.MimboxContact.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.MimboxContact;

public class AllMimboxContactsVm
{
    public IEnumerable<MimboxContactDto> MimboxContacts { get; set; }

    public AllMimboxContactsVm() => MimboxContacts = new List<MimboxContactDto>();
}
