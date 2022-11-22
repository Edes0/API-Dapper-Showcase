namespace Mimbly.Infrastructure.AAD;

using Microsoft.Graph;
using Mimbly.CoreServices.AADServices;
using Microsoft.Extensions.Logging;


public class AccountService
{

    // TODO: Create models for functions internally??
    private readonly GraphService _graphService;
    private readonly ILogger _logger;
    private readonly string _redirectUrl = "https://mimbly-frontend.azurewebsites.net";

    public AccountService(GraphService graphService, ILogger logger)
    {
        _graphService = graphService;
        _logger = logger;
    }

    // TODO: Update redirect url
    public async void InviteUser(UserInviteModel user)
    {
        var client = _graphService.GetClient();

        var invitation = new Invitation
        {
            InvitedUserDisplayName = user.DisplayName,
            InvitedUserEmailAddress = user.EmailAddress,
            InviteRedirectUrl = $"{_redirectUrl}/dashboard/{user.GroupId}",
        };

        var userInfo = new User
        {
            JobTitle = user.Contact?.JobTitle,
            MobilePhone = user.Contact?.MobilePhone,
            StreetAddress = user.Contact?.StreetAddress,
            City = user.Contact?.StreetAddress,
            Country = user.Contact?.Country
        };

        try
        {
        var invite = await client.Invitations.Request().AddAsync(invitation);

            var invitedUserId = invite.InvitedUser.Id;

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

        var invite = new Invitation
        {
            InvitedUserDisplayName = technician.DisplayName,
            InvitedUserEmailAddress = technician.EmailAddress,
            InviteRedirectUrl = _redirectUrl
        };

        var technicianInfo = new User
        {
            JobTitle = technician.Contact?.JobTitle,
            MobilePhone = technician.Contact?.MobilePhone,
            StreetAddress = technician.Contact?.StreetAddress,
            City = technician.Contact?.StreetAddress,
            Country = technician.Contact?.Country
        };

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

        var invite = new Invitation
        {
            InvitedUserDisplayName = admin.DisplayName,
            InvitedUserEmailAddress = admin.EmailAddress,
            InviteRedirectUrl = _redirectUrl
        };

        var adminInfo = new User
        {
            JobTitle = admin.Contact?.JobTitle,
            MobilePhone = admin.Contact?.MobilePhone
        };

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
        try {
            var resp = await client.Users.Request().Filter($"mail eq '{email}'").GetAsync();
            var user = resp.FirstOrDefault();
            if(user != null) return false;

            var directoryObject = new DirectoryObject {
                Id = user.Id
            };

            await client.Groups["groupId"].Members.References.Request().AddAsync(directoryObject);
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogInformation("Something went wrong creating an admin", ex);
            return false;
        }
    }
}
