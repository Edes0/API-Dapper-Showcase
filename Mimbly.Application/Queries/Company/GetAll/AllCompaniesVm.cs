namespace Mimbly.Application.Queries.Company.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.Company;

public class AllCompaniesVm
{
    public IEnumerable<CompanyContactDto> Companies { get; set; }

    public AllCompaniesVm() => Companies = new List<CompanyContactDto>();
}