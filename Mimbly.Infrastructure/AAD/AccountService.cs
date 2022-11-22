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

    public async void InviteTechnician(string displayName, string email, string phoneNumber, string companyName, string Location)
    {
        var client = _graphService.GetClient();

        var invite = new Invitation
        {
            InvitedUserDisplayName = displayName,
            InvitedUserEmailAddress = email,
            InviteRedirectUrl = $"{_redirectUrl}/dashboard"


        };

        try {
            var resp = await client.Invitations.Request().AddAsync(invite);
        }
        catch(Exception ex)
        {
            _logger.LogInformation("Something went wrong creating an admin", ex);
        }
    }

    public async void InviteAdmin(string displayName, string email)
    {
        var client = _graphService.GetClient();

        var invite = new Invitation
        {
            InvitedUserDisplayName = displayName,
            InvitedUserEmailAddress = email,
            InviteRedirectUrl = $"{_redirectUrl}/dashboard"
        };

        try {
            var resp = await client.Invitations.Request().AddAsync(invite);
        }
        catch(Exception ex)
        {
            _logger.LogInformation("Something went wrong creating an admin", ex);
        }
    }

    public async void CreateCompany(string displayName, string description, string ownerEmail, string parentCompany)
    {
        var client = _graphService.GetClient();

        var group = new Group
        {

        };

        await client.Groups.Request().AddAsync(group);
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
