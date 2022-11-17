namespace Mimbly.Api.Controllers.v1;

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
    public ActionResult InviteUser(string email, string companyId, string displayName)
    {
        try
        {
            _accountService.InviteUserToCompany(email, companyId, displayName);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Something went wrong creating a account", ex);

            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    [Route("CreateCompany")]
    [GroupsAuthorize("Admin")]
    public ActionResult CreateCompany(string companyName)
    {
        var companyId = Guid.NewGuid();
        return Created($"/Company/${companyId}", new { CompanyName = companyName, CompanyId = companyId });
    }
}
