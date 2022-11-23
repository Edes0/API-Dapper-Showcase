namespace Mimbly.Infrastructure.AAD;

public interface IAccountService
{
    Task<bool> InviteUser(UserInviteModel user);

    Task<bool> InviteTechnician(UserInviteModel technician);

    Task<bool> InviteAdmin(UserInviteModel admin);

    Task<bool> CreateCompany(UserInviteModel owner, string displayName, string description, Guid? parentCompanyId);

    Task<bool> AddUserToCompany(UserInviteModel user, Guid companyId);

}
