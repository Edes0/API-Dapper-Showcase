namespace Mimbly.Application.Queries.Company.GetWithAllDataById;

using Mimbly.Application.Contracts.Dtos.Company;

public class CompanyWithChildrenFilteredById
{
    public IEnumerable<CompanyDto> Companies { get; set; }

    public CompanyWithChildrenFilteredById() => Companies = new List<CompanyDto>();
}