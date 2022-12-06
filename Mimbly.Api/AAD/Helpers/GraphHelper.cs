namespace Mimbly.Api.AAD.Helpers;

using Microsoft.Graph;
using Mimbly.Api.AAD.DTOs;

public class GraphHelper : IGraphHelper
{
    private readonly IGraphService _graphService;

    public GraphHelper(IGraphService graphService)
    {
        _graphService = graphService;
    }

    public Invitation GetInvitation(InvitedUser user, string redirectUrl)
    {
        var invite = new Invitation
        {
            InvitedUserDisplayName = user.DisplayName,
            InvitedUserEmailAddress = user.EmailAddress,
            InviteRedirectUrl = redirectUrl,
            SendInvitationMessage = true,
            InvitedUserMessageInfo = new InvitedUserMessageInfo { MessageLanguage = "sv-SE"}
        };

        return invite;
    }

    public User GetUserInfo(InvitedUser user)
    {
        var userInfo = new User
        {
            JobTitle = user.Contact?.JobTitle,
            MobilePhone = user.Contact?.MobilePhone,
            StreetAddress = user.Contact?.StreetAddress,
            City = user.Contact?.City,
            Country = user.Contact?.Country
        };

        return userInfo;
    }

    public async Task<string?> InviteAndGetUserId(Invitation invite)
    {
        var client = _graphService.GetClient();

            var resp = await client.Invitations.Request().AddAsync(invite);
            var userId = resp.InvitedUser.Id;

            return userId;
        }

    public async void UpdateUserInfo(User userInfo, string userId)
    {
        var client = _graphService.GetClient();

            await client.Users[userId].Request().UpdateAsync(userInfo);
        }

    public async void AddMemberToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

            var dirObj = new DirectoryObject { Id = userId };
            await client.Groups[groupId].Members.References.Request().AddAsync(dirObj);
        }

    public async void AddOwnerToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

            var dirObj = new DirectoryObject { Id = userId };
            await client.Groups[groupId].Owners.References.Request().AddAsync(new DirectoryObject { Id = userId });
        }

}
