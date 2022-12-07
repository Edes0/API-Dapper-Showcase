namespace Mimbly.Api.AAD;

using Mimbly.Api.AAD.DTOs;
using Mimbly.Domain.Entities;

public interface IAccountService
{
    Task<bool> InviteUser(InvitedUser user);

    Task<bool> InviteTechnician(InvitedUser technician);

    Task<bool> InviteAdmin(InvitedUser admin);

    Task<bool> CreateCompany(CompanyModel company);

    Task<bool> AddUserToCompany(InvitedUser user, Guid companyId);

}
