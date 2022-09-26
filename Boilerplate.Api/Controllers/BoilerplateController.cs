namespace Boilerplate.Api.Controllers;

using Application.Commands.CreateBoilerPlate;
using Application.Contracts.RequestDtos;
using Application.Queries.Boilerplate;
using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class BoilerplateController : BaseController
{
    public BoilerplateController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<BoilerplatesFilteredByAgeVm>> FilterBoilerPlatesByAge([BindRequired, FromQuery] int age)
    {
        return await _mediator.Send(
            new GetBoilerplateByMinAgeQuery
            {
                Age = age
            });
    }

    [HttpPost]
    public async Task<ActionResult> CreateBoilerplate([FromBody] CreateBoilerplateRequestDto createBoilerplateRequestDto)
    {
        await _mediator.Send(
            new CreateBoilerplateCommand
            {
                CreateBoilerplateRequest = createBoilerplateRequestDto
            });

        return Ok("Boilerplate created successfully");
    }
}