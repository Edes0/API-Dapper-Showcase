namespace Boilerplate.Api.Controllers;

using Application.Commands.Emails;
using Application.Contracts.RequestDtos.Email;
using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class EmailController : BaseController
{
    public EmailController(IMediator mediator) : base(mediator)
    {
    }

    [Authorize]
    [HttpPost] // api/v1/email/users/invite
    [Route("users/invite")]
    public async Task<ActionResult<InvitedUserVm>> InviteUsers(InviteUsersRequestDto inviteUsersRequestDto)
    {
        return Ok(await _mediator.Send(new InviteUsersCommand(inviteUsersRequestDto)));
    }

    [HttpPost] // api/v1/email/user/request-reset-password
    [Route("user/request-reset-password")]
    public async Task RequestResetUserPassword(ResetUserPasswordRequestDto resetUserPasswordRequestDto)
    {
        await _mediator.Send(new ResetUserPasswordByEmailCommand(resetUserPasswordRequestDto));
    }

    [HttpPut] // api/v1/email/user/reset-password-with-token
    [Route("user/reset-password-with-token")]
    public async Task ResetUserPasswordWithToken(ResetUserPasswordWithTokenRequestDto resetUserPasswordWithTokenRequestDto)
    {
        await _mediator.Send(new ResetUserPasswordWithTokenCommand(resetUserPasswordWithTokenRequestDto));
    }
}