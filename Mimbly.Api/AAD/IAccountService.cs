namespace Mimbly.Api.AAD;

using Mimbly.Api.AAD.DTOs;
using Mimbly.Domain.Entities;

public interface IAccountService
{
    Task<bool> InviteUser(UserInviteDTO user);

    Task<bool> InviteTechnician(UserInviteDTO technician);

    Task<bool> InviteAdmin(UserInviteDTO admin);

    Task<Company> CreateCompany(UserInviteDTO owner, CreateCompanyDTO company);

    Task<bool> AddUserToCompany(UserInviteDTO user, Guid companyId);

}
