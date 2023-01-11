namespace Mimbly.Business.Interfaces.AD;

using Mimbly.Domain.Entities.AD;

public interface IAccountService
{
    Task<bool> InviteUser(AdUser user);
    Task<string?> CreateCompany(AdCompany company);
    Task<bool> AddUserToCompany(AdUser user, Guid companyId);
    Task RemoveCompany(Guid id);
}
