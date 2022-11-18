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
    public async void InviteUser(string email, string companyId, string? displayName)
    {
        var client = _graphService.GetClient();

        var invitation = new Invitation
        {
            InvitedUserDisplayName = displayName,
            InvitedUserEmailAddress = email,
            InviteRedirectUrl = $"{_redirectUrl}/dashboard/{companyId}"
        };

        var invite = await client.Invitations.Request().AddAsync(invitation);

        var newUserId = invite.InvitedUser.Id;
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
