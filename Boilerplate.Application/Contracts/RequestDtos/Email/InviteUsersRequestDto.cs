namespace Boilerplate.Application.Contracts.RequestDtos.Email;
using Boilerplate.Domain.ApplicationModels.Email;

public class InviteUsersRequestDto
{
    public InviteUsersRequestDto(IEnumerable<InvitedUserModel> users) => Users = users;

    public IEnumerable<InvitedUserModel> Users { get; set; }
}