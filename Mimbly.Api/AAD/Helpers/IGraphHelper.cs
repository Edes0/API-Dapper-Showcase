namespace Mimbly.Api.AAD.Helpers;

using System.Threading.Tasks;
using Microsoft.Graph;
using Mimbly.Api.AAD.DTOs;

public interface IGraphHelper
{
    User GetUserInfo(InvitedUser user);
    Invitation GetInvitation(InvitedUser user, string redirectUrl);
    void AddMemberToGroup(string groupId, string userId);
    void AddOwnerToGroup(string groupId, string userId);
    Task<string?> InviteAndGetUserId(Invitation invite);
    void UpdateUserInfo(User userInfo, string userId);
}