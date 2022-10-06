namespace Mimbly.Api.Controllers;

using Application.Commands.CreateMimbly;
using Application.Contracts.RequestDtos;
using Application.Queries.Mimbly;
using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Queries.Mimbox.GetAll;

[ApiController]
//[Authorize] //TODO: LIsta ur hur man använder authorization
[Route("api/v1/[controller]")]
public class MimboxController : BaseController
{
    public MimboxController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<MimboxesNotFiltered>> GetAllMimboxes()
    {
        return Ok(await _mediator.Send(new GetAllMimboxesQuery { }));
    }

    [Route("byage")]
    [HttpGet]
    public async Task<ActionResult<MimblysFilteredByAgeVm>> FilterMimblysByAge([BindRequired, FromQuery] int age)
    {
        return await _mediator.Send(
            new GetMimblyByMinAgeQuery
            {
                Age = age
            });
    }

    [HttpPost]
    public async Task<ActionResult> CreateMimbly([FromBody] CreateMimboxRequestDto createMimblyRequestDto)
    {
        await _mediator.Send(
            new CreateMimboxCommand
            {
                CreateMimboxRequest = createMimblyRequestDto
            });

        return Ok("Mimbly created successfully");
    }
}