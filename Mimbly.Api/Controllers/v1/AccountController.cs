namespace Mimbly.Api.Controllers.v1;

using Application.Commands.AD.InviteUserToAd;
using Application.Contracts.Dtos.AD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Mimbly.Application.Queries.AD.GetRoles;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("InviteUser")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteUser(InviteUserRequestDto inviteUserRequestDto)
    {
        var status = await _mediator.Send(new InviteUserToAdCommand { InviteUserRequestToAdRequest = inviteUserRequestDto });

        return status ? Ok() : BadRequest();
    }

    [HttpGet]
    [Route("GetRoles")]
    public async Task<ActionResult> GetRoles()
    {
        var roles = await _mediator.Send(new GetRolesQuery());

        return Ok(roles.Roles);
    }
}
