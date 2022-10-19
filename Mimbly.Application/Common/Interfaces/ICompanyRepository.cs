namespace Mimbly.Application.Common.Interfaces;

using Mimbly.Domain.Entities;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompanies();

    Task<IEnumerable<Company>> GetCompanyById(Guid Id);

    Task<IEnumerable<Company>> GetCompanyWithChildrenById(Guid id);

    Task CreateCompany(Company company);

    Task DeleteCompany(Company company);
 }