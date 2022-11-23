﻿namespace Mimbly.Api.Controllers.v1;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimbly.CoreServices.Authorization;
using Mimbly.Infrastructure.AAD;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController : ControllerBase
{

    // TODO: Add validation

    private readonly AccountService _accountService;
    private readonly ILogger _logger;

    public AccountController(AccountService accountService, ILogger logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpPost]
    [Route("InviteUser")]
    [GroupsAuthorize("Admin")]
    public async Task<ActionResult> InviteUser(UserInviteModel user)
    {
        var status = await _accountService.InviteUser(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteTechnician")]
    [GroupsAuthorize("Admin")]
    public async Task<ActionResult> InviteTechnician(UserInviteModel user)
    {
        var status = await _accountService.InviteTechnician(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteAdmin")]
    [GroupsAuthorize("Admin")]
    public async Task<ActionResult> InviteAdmin(UserInviteModel user)
    {
        var status = await _accountService.InviteAdmin(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("CreateCompany")]
    [GroupsAuthorize("Admin")]
    public async Task<ActionResult> CreateCompany(UserInviteModel owner, string name, string desc, Guid? parent)
    {
        var status = await _accountService.CreateCompany(owner, name, desc, parent);

        return status ? Ok() : BadRequest();
    }
}
