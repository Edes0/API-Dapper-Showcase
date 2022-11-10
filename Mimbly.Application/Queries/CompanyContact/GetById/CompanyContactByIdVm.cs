namespace Mimbly.Application.Queries.CompanyContact.GetById;

using Mimbly.Application.Contracts.Dtos.CompanyContact;

public class CompanyContactByIdVm
{
    public CompanyContactDto CompanyContact { get; set; }

    public CompanyContactByIdVm() => CompanyContact = new CompanyContactDto();
}