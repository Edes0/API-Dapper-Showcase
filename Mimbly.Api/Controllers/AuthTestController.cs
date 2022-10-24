namespace Mimbly.Api.Controllers;
using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/authtest")]
public class AuthTestController : BaseController
{
    public AuthTestController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        return Ok(new { message = "Your token gave you authorization!" });
    }
}
