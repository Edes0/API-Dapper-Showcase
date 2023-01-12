namespace Mimbly.Api.Controllers.v1;

using Application.Commands.AD.InviteUserToAd;
using Application.Contracts.Dtos.AD;
using CoreServices.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Mimbly.Application.Queries.AD.GetRoles;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMemoryCache _memoryCache;

    public AccountController(IMediator mediator, IMemoryCache memoryCache)
    {
        _mediator = mediator;
        _memoryCache = memoryCache;
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
        var rolesInCache = _memoryCache.Get<IEnumerable<RoleDto>>("Roles");
        if (rolesInCache != null) return Ok(rolesInCache);

        var memoryCacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(12));
        var roles = await _mediator.Send(new GetRolesQuery());
        _memoryCache.Set<IEnumerable<RoleDto>>("Roles", roles.Roles, memoryCacheOptions);
        return Ok(roles.Roles);
    }
}
