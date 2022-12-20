namespace Mimbly.Api.Controllers.v1;

using Application.Commands.AD.AddCompanyToAd;
using Application.Commands.AD.InviteUserToAd;
using Application.Contracts.Dtos.AD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> InviteUser(InviteUserDto inviteUserDto)
    {
        var status = await _mediator.Send(new InviteUserToAdCommand { InviteUserToAdRequest = inviteUserDto });

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
    public async Task<ActionResult> CreateCompany(AddCompanyDto addCompanyDto)
    {
        var groupId = await _mediator.Send(new AddCompanyToAdCommand {AddCompanyToAdRequest = addCompanyDto});

        if (Guid.TryParse(groupId, out var groupGuid))
        {
            var createdCompany = await _mediator.Send(new CreateCompanyCommand { CreateCompanyRequest = new CreateCompanyRequestDto { Name = addCompanyDto.CompanyName, Id = groupGuid, ParentId = addCompanyDto.ParentId } });

            return new CreatedAtRouteResult("CompanyById", new { controller = "Company", id = createdCompany.Id }, createdCompany);
        }
        else
        {
            return BadRequest();
        }
    }
}
