namespace Mimbly.Infrastructure.AAD;

using Microsoft.Graph;
using Mimbly.CoreServices.AADServices;

public class AccountService
{
    private readonly GraphService _graphService;

    public AccountService(GraphService graphService) => _graphService = graphService;

    // TODO: Update redirect url
    public async void InviteUserToCompany(string email, string companyId, string? displayName)
    {
        var client = _graphService.GetClient();

        var invitation = new Invitation
        {
            InvitedUserDisplayName = displayName,
            InvitedUserEmailAddress = email,
            InviteRedirectUrl = $"https://mimbly-frontend.azurewebsites.net/dashboard/{companyId}"
        };

        var invite = await client.Invitations.Request().AddAsync(invitation);

        var newUserId = invite.InvitedUser.Id;
    }

    public async void CreateCompany()
    {
        var client = _graphService.GetClient();

        var group = new Group
        {

        };

        await client.Groups.Request().AddAsync(group);
    }
}
