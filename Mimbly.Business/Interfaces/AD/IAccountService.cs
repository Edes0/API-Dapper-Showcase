namespace Mimbly.Business.Interfaces.AD;

using Mimbly.Domain.Entities.AD;

public interface IAccountService
{
    Task<bool> InviteUser(AdUser user);
    Task<bool> InviteTechnician(AdUser technician);
    Task<bool> InviteAdmin(AdUser admin);
    Task<string?> CreateCompany(AdCompany company);
    Task<bool> AddUserToCompany(AdUser user, Guid companyId);
    Task RemoveCompany(Guid id);
}
