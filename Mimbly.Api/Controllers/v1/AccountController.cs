namespace Mimbly.Api.Controllers.v1;

using Application.Commands.AD.AddCompanyToAd;
using Application.Commands.AD.InviteUserToAd;
using Application.Contracts.Dtos.AD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimbly.Application.Commands.Company.CreateCompany;
using Mimbly.Application.Contracts.Dtos.Company;
using MediatR;

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
    public async Task<ActionResult> InviteUser(InviteUserDto inviteUserDto)
    {
        var status = await _mediator.Send(new InviteUserToAdCommand { InviteUserToAdRequest = inviteUserDto });

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteTechnician")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteTechnician(InviteUserDto userDto)
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("InviteAdmin")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteAdmin(InviteUserDto userDto)
    {
        return BadRequest();
    }
}
