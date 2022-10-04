namespace Mimbly.Application.Commands.Emails;

using Contracts.RequestDtos.Email;
using MediatR;

public class InviteUsersCommand : IRequest<InvitedUserVm>
{
    public InviteUsersCommand(InviteUsersRequestDto inviteUsersRequestDto) => InviteUsersRequestDto = inviteUsersRequestDto;
    public InviteUsersRequestDto InviteUsersRequestDto { get; set; }
}