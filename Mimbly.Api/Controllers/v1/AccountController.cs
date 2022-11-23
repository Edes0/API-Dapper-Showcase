namespace Mimbly.Api.Controllers.v1;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimbly.CoreServices.Authorization;
using Mimbly.Api.AAD;
using Mimbly.Api.AAD.DTOs;

[ApiController]
/*[Authorize]*/
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController : ControllerBase
{

    // TODO: Add validation

    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("InviteUser")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteUser(UserInviteDTO user)
    {
        await user.Validate();

        var status = await _accountService.InviteUser(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteTechnician")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteTechnician(UserInviteDTO user)
    {
        await user.Validate();

        var status = await _accountService.InviteTechnician(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteAdmin")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteAdmin(UserInviteDTO user)
    {
        await user.Validate();

        var status = await _accountService.InviteAdmin(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("CreateCompany")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> CreateCompany(UserInviteDTO owner, CreateCompanyDTO company)
    {
        await owner.Validate();
        await company.Validate();

        var createdCompany = await _accountService.CreateCompany(owner, company);

        if (createdCompany != null)
        {
            return Created($"/Company/{createdCompany.Id}", createdCompany);
        }
        else
        {
            return BadRequest();
        }
    }
}
