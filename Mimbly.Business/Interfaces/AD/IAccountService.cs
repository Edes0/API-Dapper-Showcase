namespace Mimbly.Api.AAD;

using Domain.Entities.AD;
using Mimbly.Domain.Entities;

public interface IAccountService
{
    Task<bool> InviteUser(AdUser user);
    Task<bool> InviteTechnician(AdUser technician);
    Task<bool> InviteAdmin(AdUser admin);
    Task<string?> CreateCompany(AdCompany company);
    Task<bool> AddUserToCompany(AdUser user, Guid companyId);
}
