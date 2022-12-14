namespace Mimbly.Api.Controllers.v1;

using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
[Authorize]
public class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Route("ValidateUser")]
    public IActionResult ValidateUser()
    {
        var Claims = HttpContext.User;

        var Email = Claims.FindFirstValue(ClaimTypes.Email);
        var FirstName = Claims.FindFirstValue(ClaimTypes.GivenName);
        var LastName = Claims.FindFirstValue(ClaimTypes.Surname);
        var Roles = Claims.FindAll(ClaimTypes.Role).Select((rc) => rc.Value).ToList();

        return Ok(new { Email, FirstName, LastName, Roles });
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(new { message = "Your token gave you authorization!" });
    }
}
