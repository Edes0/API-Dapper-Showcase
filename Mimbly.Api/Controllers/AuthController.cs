namespace Mimbly.Api.Controllers;

using System.Security.Claims;
using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/auth")]
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
        List<string> Roles = Claims.FindAll(ClaimTypes.Role).Select((rc) => rc.Value).ToList();

        return Ok(new {Email = Email, FirstName = FirstName, LastName = LastName, Roles = Roles});
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(new { message = "Your token gave you authorization!" });
    }
}
