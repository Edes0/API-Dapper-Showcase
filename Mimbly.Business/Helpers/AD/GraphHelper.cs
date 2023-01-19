namespace Mimbly.Business.Helpers.AD;

using Domain.Entities.AD;
using Interfaces.AD;
using Microsoft.Graph;

public class GraphHelper : IGraphHelper
{
    private readonly IGraphService _graphService;

    public GraphHelper(IGraphService graphService)
    {
        _graphService = graphService;
    }

    /// <summary>
    /// Method <c>GetInvitation</c> creates a Graph <c>Invitation</c> from
    /// provided <c>AdUser</c> and <c>redirectUrl</c>.
    /// </summary>
    /// <param name="user">The <c>AdUser</c> that contains user information.</param>
    /// <param name="redirectUrl">The url to redirect to once invite is accepted.</param>
    /// <returns>An <c>Invitation</c>.</returns>
    public Invitation GetInvitation(AdUser user, string redirectUrl)
    {
        var invite = new Invitation
        {
            InvitedUserDisplayName = user.DisplayName,
            InvitedUserEmailAddress = user.Email,
            InviteRedirectUrl = redirectUrl,
            SendInvitationMessage = true,
            InvitedUserMessageInfo = new InvitedUserMessageInfo { MessageLanguage = "sv-SE" }
        };

        return invite;
    }

    /// <summary>
    /// Method <c>GetUserInfo</c> creates a Graph <c>User</c> from
    /// <c>AdUser</c>.
    /// </summary>
    /// <param name="user">The <c>AdUser</c> that contains user information.</param>
    /// <returns>A Graph <c>User</c> object.</returns>
    public User GetUserInfo(AdUser user)
    {
        var userInfo = new User
        {
            JobTitle = user.JobTitle,
            MobilePhone = user.Phone,
            StreetAddress = user.StreetAddress,
            City = user.City,
            Country = user.Country
        };

        return userInfo;
    }

    /// <summary>
    /// Method <c>InviteAndGetUserId</c> sends the provided <c>Invitation</c>
    /// and returns it with data from Ad.
    /// </summary>
    /// <param name="invite">The Graph <c>Invitation</c>.</param>
    /// <returns>A created users Guid.</returns>
    public async Task<string?> InviteAndGetUserId(Invitation invite)
    {
        var client = _graphService.GetClient();

        var resp = await client.Invitations.Request().AddAsync(invite);
        var userId = resp.InvitedUser.Id;

        return userId;
    }

    /// <summary>
    /// Method <c>UpdateUserInfo</c> updates user information
    /// on Azure Ad with provided information.
    /// </summary>
    /// <param name="userInfo">A Graph <c>User</c> object containing the new information.</param>
    /// <param name="userId">The user to update the information for.</param>
    public async void UpdateUserInfo(User userInfo, string userId)
    {
        var client = _graphService.GetClient();
        try
        {
            await client.Users[userId].Request().UpdateAsync(userInfo);
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// Method <c>AddMemberToGroup</c> adds a specified user to a
    /// specified group.
    /// </summary>
    /// <param name="groupId">The id to the group the user is getting added to.</param>
    /// <param name="userId">The id of the user to be added to the group.</param>
    public async void AddMemberToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

        var dirObj = new DirectoryObject { Id = userId };

        try
        {
            await client.Groups[groupId].Members.References.Request().AddAsync(dirObj);
        }
        catch (ServiceException ex)
        {

        }
    }

    /// <summary>
    /// Method <c>AddOwnerToGroup</c> adds a owner to a group.
    /// </summary>
    /// <param name="groupId">The id to the group the user is getting added to.</param>
    /// <param name="userId">The id of the user to be added to the group.</param>
    public async void AddOwnerToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

        var dirObj = new DirectoryObject { Id = userId };
        await client.Groups[groupId].Owners.References.Request().AddAsync(dirObj);
    }

    /// <summary>
    /// Method <c>GetGroupsThatStartsWith</c> filters out the
    /// groups based on the phrase provided.
    /// </summary>
    /// <param name="phrase">The phrase that is used to filter out group.</param>
    /// <returns>A <c>IEnumerable</c> of <c>Group</c>.</returns>
    public async Task<IEnumerable<Group>> GetGroupsThatStartsWith(string phrase)
    {
        var client = _graphService.GetClient();

        var groups = await client.Groups.Request().Filter($"startsWith(displayName, '{phrase}')").GetAsync();

        return groups;
    }
}
