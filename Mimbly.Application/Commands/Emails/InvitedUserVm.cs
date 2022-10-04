namespace Mimbly.Application.Commands.Emails;

using Domain.ApplicationModels.Email;

public class InvitedUserVm
{
    public InvitedUserVm(IEnumerable<InvitedUserModel> users) => Users = users;

    public IEnumerable<InvitedUserModel> Users { get; set; }
}