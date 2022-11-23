namespace Mimbly.Infrastructure.AAD;

using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Mimbly.CoreServices.AADServices;


public class AccountService : IAccountService
{
    private readonly GraphService _graphService;
    private readonly ILogger _logger;
    private readonly UriBuilder _redirectUrl = new("https://mimbly-frontend.azurewebsites.net");

    public AccountService(GraphService graphService, ILogger logger)
    {
        _graphService = graphService;
        _logger = logger;
    }

    public async Task<bool> InviteUser(UserInviteModel user)
    {
        var redirectUrl = _redirectUrl.Path = "dashboard/" + user.GroupId;

        var userInvitation = GetInvitation(user, redirectUrl);
        var userInfo = GetUserInfo(user);

        var invitedUserId = await InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            UpdateUserInfo(userInfo, invitedUserId);
            AddMemberToGroup(user.GroupId, invitedUserId);

            return true;
        }

        return false;
    }

    public async Task<bool> InviteTechnician(UserInviteModel technician)
    {
        var userInvitation = GetInvitation(technician, _redirectUrl.ToString());
        var userInfo = GetUserInfo(technician);

        var invitedUserId = await InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            UpdateUserInfo(userInfo, invitedUserId);

            return true;
        }

        return false;

        // TODO: Add technician to database, once entity is created
    }

    public async Task<bool> InviteAdmin(UserInviteModel admin)
    {
        var userInvitation = GetInvitation(admin, _redirectUrl.ToString());
        var userInfo = GetUserInfo(admin);

        var invitedUserId = await InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            UpdateUserInfo(userInfo, invitedUserId);

            // TODO: Insert admin group id, best case have in memory dicitionary of companyName and Ids.
            AddMemberToGroup("Admin group Id", invitedUserId);

            return true;
        }

        return false;
    }

    public async Task<bool> CreateCompany(UserInviteModel owner, string displayName, string description, Guid? parentCompanyId)
    {
        // TODO: Create company instance in Db, Handle parentCompany relation
        var client = _graphService.GetClient();

        var redirectUrl = _redirectUrl.Path = "dashboard/" + owner.GroupId;

        var userInvitation = GetInvitation(owner, redirectUrl);
        var userInfo = GetUserInfo(owner);

        var groupInfo = new Group
        {
            DisplayName = displayName,
            Description = description,
        };

        var group = await client.Groups.Request().AddAsync(groupInfo);
        var invitedUserId = await InviteAndGetUserId(userInvitation);

        if (invitedUserId != null)
        {
            UpdateUserInfo(userInfo, invitedUserId);
            AddOwnerToGroup(group.Id, invitedUserId);

            return true;
        }

        return false;
    }

    public Task<bool> AddUserToCompany(UserInviteModel user, Guid companyId) => throw new NotImplementedException();

    public static Invitation GetInvitation(UserInviteModel user, string redirectUrl)
    {
        var invite = new Invitation
        {
            InvitedUserDisplayName = user.DisplayName,
            InvitedUserEmailAddress = user.EmailAddress,
            InviteRedirectUrl = redirectUrl
        };

        return invite;
    }

    public static User GetUserInfo(UserInviteModel user)
    {
        var userInfo = new User
        {
            JobTitle = user.Contact?.JobTitle,
            MobilePhone = user.Contact?.MobilePhone,
            StreetAddress = user.Contact?.StreetAddress,
            City = user.Contact?.StreetAddress,
            Country = user.Contact?.Country
        };

        return userInfo;
    }

    public async Task<string?> InviteAndGetUserId(Invitation invite)
    {
        var client = _graphService.GetClient();

        try
        {
            var resp = await client.Invitations.Request().AddAsync(invite);
            var userId = resp.InvitedUser.Id;

            return userId;
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension
            _logger.LogInformation("Something went wrong inviting a user: ", ex);
            return null;
        }
    }

    public async void UpdateUserInfo(User userInfo, string userId)
    {
        var client = _graphService.GetClient();

        try
        {
            await client.Users[userId].Request().UpdateAsync(userInfo);
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension
            _logger.LogInformation("Something went wrong inviting a user: ", ex);
        }
    }

    public async void AddMemberToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

        try
        {
            var dirObj = new DirectoryObject { Id = userId };
            await client.Groups[groupId].Members.References.Request().AddAsync(dirObj);
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension
            _logger.LogInformation("Something went wrong adding a member to a group: ", ex);
        }
    }

    public async void AddOwnerToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

        try
        {
            var dirObj = new DirectoryObject { Id = userId };
            await client.Groups[groupId].Owners.References.Request().AddAsync(new DirectoryObject { Id = userId });
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension
            _logger.LogInformation("Something went wrong adding a member to a group: ", ex);
        }
    }

    
}
