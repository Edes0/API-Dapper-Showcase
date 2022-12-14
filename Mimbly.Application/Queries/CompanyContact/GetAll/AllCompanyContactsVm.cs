namespace Mimbly.Application.Queries.CompanyContact.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.CompanyContact;

public class AllCompanyContactsVm
{
    public IEnumerable<CompanyContactDto> CompanyContacts { get; set; }

    public AllCompanyContactsVm() => CompanyContacts = new List<CompanyContactDto>();
}
