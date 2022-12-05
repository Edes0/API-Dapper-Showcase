namespace Mimbly.Api.Controllers.v1;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimbly.CoreServices.Authorization;
using Mimbly.Api.AAD;
using Mimbly.Api.AAD.DTOs;
using AutoMapper;

[ApiController]
/*[Authorize]*/
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController : ControllerBase
{

    // TODO: Add validation

    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("InviteUser")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteUser(UserInviteDTO userDto)
    {
        await userDto.Validate();

        var user = _mapper.Map<InvitedUser>(userDto);
        var status = await _accountService.InviteUser(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteTechnician")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteTechnician(InvitedUser userDto)
    {
        await userDto.Validate();

        var status = await _accountService.InviteTechnician(userDto);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteAdmin")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteAdmin(InvitedUser userDto)
    {
        await userDto.Validate();

        var status = await _accountService.InviteAdmin(userDto);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("CreateCompany")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> CreateCompany(CreateCompanyDTO createCompanyDto)
    {
        await createCompanyDto.user.Validate();
        await createCompanyDto.company.Validate();

        var createdCompany = await _accountService.CreateCompany(createCompanyDto.user, createCompanyDto.company);

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
