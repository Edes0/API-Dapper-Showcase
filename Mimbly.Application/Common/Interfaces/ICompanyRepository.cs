namespace Mimbly.Application.Common.Interfaces;

using Mimbly.Domain.Entities;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompanies();
    Task<Company> GetCompanyById(Guid id);
    Task<IEnumerable<Company>> GetParentAndChildrenIdsById(Guid id);
    Task<IEnumerable<Company>> GetCompanyByIds(IEnumerable<Guid> ids);
    Task CreateCompany(Company company);
    Task DeleteCompany(Company company);
    Task UpdateCompany(Company company);
 }