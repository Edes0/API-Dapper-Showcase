namespace Mimbly.Api.Controllers;

using Application.Commands.Mimbox.CreateMimbox;
using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.Mimbox.DeleteMimbox;
using Mimbly.Application.Contracts.Dtos.Mimbox;
using Mimbly.Application.Queries.Mimbox.GetAll;
using Mimbly.Application.Queries.Mimbox.GetByAge;
using Mimbly.Application.Queries.Mimbox.GetById;

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

    [Route("ById")]
    public async Task<ActionResult<MimboxFilteredById>> FilterMimboxesById([BindRequired, FromQuery] Guid id)
    {
        return Ok(await _mediator.Send(new GetFilterByIdMimboxQuery { Id = id }));
    }

    [Route("ByAge")]
    [HttpGet]
    public async Task<ActionResult<MimboxesFilteredByAge>> FilterMimboxesByAge([BindRequired, FromQuery] int age)
    {
        return Ok(await _mediator.Send(new GetFilterByAgeMimboxQuery { Age = age }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateMimbox([FromBody] CreateMimboxRequestDto createMimboxRequestDto)
    {
        await _mediator.Send(new CreateMimboxCommand { CreateMimboxRequest = createMimboxRequestDto });

        return Ok("Mimbox created successfully");
    }

    [HttpDelete]//("{id:guid}")]
    public async Task<ActionResult> DeleteMimbox([BindRequired, FromQuery] Guid id)
    {
        await _mediator.Send(new DeleteMimboxCommand { Id = id });

        return Ok("Mimbox removed successfully");
}
}