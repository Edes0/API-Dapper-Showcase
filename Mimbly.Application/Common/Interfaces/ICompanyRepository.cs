namespace Mimbly.Application.Common.Interfaces;

using Mimbly.Domain.Entities;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompanies();
    Task<Company> GetCompanyById(Guid id);
    Task<IEnumerable<Company>> GetParentWithChildrenById(Guid id);
    Task<IEnumerable<Company>> GetCompanyDataById(IEnumerable<Guid> ids);
    Task CreateCompany(Company company);
    Task DeleteCompany(Company company);
    Task UpdateCompany(Company company);
 }