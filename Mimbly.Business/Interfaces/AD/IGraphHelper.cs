namespace Mimbly.Business.Interfaces.AD;

using System.Threading.Tasks;
using Domain.Entities.AD;
using Microsoft.Graph;

public interface IGraphHelper
{
    User GetUserInfo(AdUser user);
    Invitation GetInvitation(AdUser user, string redirectUrl);
    void AddMemberToGroup(string groupId, string userId);
    void AddOwnerToGroup(string groupId, string userId);
    Task<string?> InviteAndGetUserId(Invitation invite);
    void UpdateUserInfo(User userInfo, string userId);
    Task<IEnumerable<Group>> GetGroupsThatStartsWith(string phrase);
}