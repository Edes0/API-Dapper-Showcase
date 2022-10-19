namespace Mimbly.Application.Queries.Company.GetAll;

using System.Collections.Generic;
using Mimbly.Application.Contracts.Dtos.Company;

public class CompaniesNotFiltered
{
    public IEnumerable<CompanyDto> Companies { get; set; }

    public CompaniesNotFiltered() => Companies = new List<CompanyDto>();
}