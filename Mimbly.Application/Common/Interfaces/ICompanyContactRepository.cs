namespace Mimbly.Application.Common.Interfaces;

using Mimbly.Domain.Entities;

public interface ICompanyContactRepository
{
    Task<IEnumerable<CompanyContact>> GetAllCompanyContacts();
    Task<CompanyContact> GetCompanyContactById(Guid id);
    Task CreateCompanyContact(CompanyContact companyContact);
    Task DeleteCompanyContact(CompanyContact companyContact);
    Task UpdateCompanyContact(CompanyContact companyContact);
    Task<IEnumerable<CompanyContact>> GetCompanyContactsByCompanyIds(IEnumerable<Guid> ids);
    Task<IEnumerable<CompanyContact>> GetCompanyContactsByCompanyId(Guid id);
}