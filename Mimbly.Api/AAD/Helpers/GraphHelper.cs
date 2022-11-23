namespace Mimbly.Api.AAD.Helpers;

using Microsoft.Graph;
using Mimbly.Api.AAD.DTOs;

public class GraphHelper : IGraphHelper
{
    private readonly IGraphService _graphService;
    private readonly ILogger<GraphHelper> _logger;

    public GraphHelper(IGraphService graphService, ILogger<GraphHelper> logger)
    {
        _graphService = graphService;
        _logger = logger;
    }

    public Invitation GetInvitation(UserInviteDTO user, string redirectUrl)
    {
        var invite = new Invitation
        {
            InvitedUserDisplayName = user.DisplayName,
            InvitedUserEmailAddress = user.EmailAddress,
            InviteRedirectUrl = redirectUrl
        };

        return invite;
    }

    public User GetUserInfo(UserInviteDTO user)
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
