namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v{version:apiVersion}/ping")]
[ApiVersion("1.0")]
public class PingController : BaseController
{
    public PingController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Route("")]
    public ActionResult Ping()
    {
        return Ok(new { message = "Welcome to Mimbly api service.", sent = DateTime.Now });
    }
}