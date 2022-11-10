namespace Mimbly.Application.Queries.Company.GetById;

using Mimbly.Application.Contracts.Dtos.Company;

public class CompanyByIdVm
{
    public CompanyContactDto Company { get; set; }

    public CompanyByIdVm() => Company = new CompanyContactDto();
}