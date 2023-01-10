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
    public async Task<ActionResult> InviteUser(InviteUserRequestDto inviteUserRequestDto)
    {
        var status = await _mediator.Send(new InviteUserToAdCommand { InviteUserRequestToAdRequest = inviteUserRequestDto });

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteTechnician")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteTechnician(InviteUserRequestDto userRequestDto)
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("InviteAdmin")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteAdmin(InviteUserRequestDto userRequestDto)
    {
        return BadRequest();
    }
}
