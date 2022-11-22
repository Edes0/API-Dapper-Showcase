namespace Mimbly.Infrastructure.AAD;

using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Mimbly.CoreServices.AADServices;


public class AccountService
{

    // TODO: Create models for functions internally??
    private readonly GraphService _graphService;
    private readonly ILogger _logger;
    private readonly UriBuilder _redirectUrl = new("https://mimbly-frontend.azurewebsites.net");

    public AccountService(GraphService graphService, ILogger logger)
    {
        _graphService = graphService;
        _logger = logger;
    }

    // TODO: Update redirect url
    public async void InviteUser(UserInviteModel user)
    {
        var client = _graphService.GetClient();
        var redirectUrl = _redirectUrl.Path = "dashboard/" + user.GroupId;

        var invite = GetInvitation(user, redirectUrl);
        var userInfo = GetUserInfo(user);

        try
        {
            var resp = await client.Invitations.Request().AddAsync(invite);

            var invitedUserId = resp.InvitedUser.Id;

            await client.Users[invitedUserId].Request().UpdateAsync(userInfo);
            await client.Groups[user.GroupId].Members.References.Request().AddAsync(new DirectoryObject { Id = invitedUserId });
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension 
            _logger.LogInformation("Something went wrong inviting a user.", ex);
        }
    }

    public async void InviteTechnician(UserInviteModel technician)
    {
        var client = _graphService.GetClient();

        var invite = GetInvitation(technician, _redirectUrl.ToString());
        var technicianInfo = GetUserInfo(technician);

        try
        {
            var resp = await client.Invitations.Request().AddAsync(invite);

            var invitedUserId = invite.InvitedUser.Id;

            await client.Users[invitedUserId].Request().UpdateAsync(technicianInfo);

            // TODO: Add technician to database, once entity is created
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension 
            _logger.LogInformation("Something went wrong inviting a technician.", ex);
        }
    }

    public async void InviteAdmin(UserInviteModel admin)
    {
        var client = _graphService.GetClient();

        var invite = GetInvitation(admin, _redirectUrl.ToString());
        var adminInfo = GetUserInfo(admin);

        try
        {
            var resp = await client.Invitations.Request().AddAsync(invite);

            var invitedUserId = resp.InvitedUser.Id;

            // TODO: Insert admin group id, best case have in memory dicitionary of companyName and Ids.
            await client.Users[invitedUserId].Request().UpdateAsync(adminInfo);
            await client.Groups["Insert admin id here"].Members.References.Request().AddAsync(new DirectoryObject { Id = invitedUserId });
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension 
            _logger.LogInformation("Something went wrong creating an admin", ex);
        }
    }

    public async void CreateCompany(UserInviteModel owner, string displayName, string description, Guid parentCompanyId)
    {
        // TODO: Handle parentCompanyConnection, Create company instance in Db
        var client = _graphService.GetClient();

        var invitation = new Invitation
        {
            InvitedUserDisplayName = owner.DisplayName,
            InvitedUserEmailAddress = owner.EmailAddress,
            InviteRedirectUrl = $"{_redirectUrl}/dashboard/{owner.GroupId}",
        };

        var userInfo = new User
        {
            JobTitle = owner.Contact?.JobTitle,
            MobilePhone = owner.Contact?.MobilePhone,
            StreetAddress = owner.Contact?.StreetAddress,
            City = owner.Contact?.StreetAddress,
            Country = owner.Contact?.Country
        };

        var group = new Group
        {
            DisplayName = displayName,
            Description = description,
        };

        try
        {
            var resp = await client.Groups.Request().AddAsync(group);
            var invite = await client.Invitations.Request().AddAsync(invitation);

            var groupId = resp.Id;
            var invitedUserId = invite.InvitedUser.Id;

            await client.Users[invitedUserId].Request().UpdateAsync(userInfo);
            await client.Groups[groupId].Owners.References.Request().AddAsync(new DirectoryObject { Id = invitedUserId });
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension 
            _logger.LogInformation("Something went wrong creating an admin", ex);
        }
    }

    public async Task<bool> AssignRole(string email, string groupId)
    {
        var client = _graphService.GetClient();
        try
        {
            var resp = await client.Users.Request().Filter($"mail eq '{email}'").GetAsync();
            var user = resp.FirstOrDefault();
            if (user != null)
            {
                return false;
            }

            var directoryObject = new DirectoryObject
            {
                Id = user.Id
            };

            await client.Groups[groupId].Members.References.Request().AddAsync(directoryObject);
            return true;
        }
        catch (Exception ex)
        {
            // TODO: create a LoggerMessage Extension 
            _logger.LogInformation("Something went wrong creating an admin", ex);
            return false;
        }
    }

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
}
