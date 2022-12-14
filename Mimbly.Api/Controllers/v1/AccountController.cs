namespace Mimbly.Api.Controllers.v1;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mimbly.CoreServices.Authorization;
using Mimbly.Api.AAD;
using Mimbly.Api.AAD.DTOs;
using AutoMapper;
using Mimbly.Application.Commands.Company.CreateCompany;
using Mimbly.Application.Contracts.Dtos.Company;
using MediatR;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AccountController(IAccountService accountService, IMapper mapper,
        IMediator mediator)
    {
        _accountService = accountService;
        _mapper = mapper;
        _mediator = mediator;
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
    public async Task<ActionResult> InviteTechnician(UserInviteDTO userDto)
    {
        await userDto.Validate();

        var user = _mapper.Map<InvitedUser>(userDto);
        var status = await _accountService.InviteTechnician(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("InviteAdmin")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> InviteAdmin(UserInviteDTO userDto)
    {
        await userDto.Validate();

        var user = _mapper.Map<InvitedUser>(userDto);
        var status = await _accountService.InviteAdmin(user);

        return status ? Ok() : BadRequest();
    }

    [HttpPost]
    [Route("CreateCompany")]
    /*[GroupsAuthorize("Admin")]*/
    public async Task<ActionResult> CreateCompany(CreateCompanyDTO createCompanyDto)
    {
        await createCompanyDto.Validate();

        var company = _mapper.Map<CompanyModel>(createCompanyDto);
        var isCreated = await _accountService.CreateCompany(company);

        if (isCreated)
        {
            await _mediator.Send(new CreateCompanyCommand { CreateCompanyRequest = new CreateCompanyRequestDto { Name = company.Name, ParentId = company.ParentId } });
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}
