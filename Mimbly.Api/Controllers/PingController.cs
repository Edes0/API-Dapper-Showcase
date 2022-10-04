namespace Mimbly.Api.Controllers;

using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/ping")]
public class PingController : BaseController
{
    public PingController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Route("")]
    public ActionResult Ping()
    {
        return Ok("Welcome to Mimbly api service.");
    }
}