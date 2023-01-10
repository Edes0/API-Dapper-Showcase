namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.MimboxModel.CreateMimboxModel;
using Mimbly.Application.Commands.MimboxModel.DeleteMimboxModel;
using Mimbly.Application.Commands.MimboxModel.UpdateMimboxModel;
using Mimbly.Application.Contracts.Dtos.MimboxModel;
using Mimbly.Application.Queries.MimboxModel.GetAll;
using Mimbly.Application.Queries.MimboxModel.GetById;

[ApiController]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]

public class MimboxModelController : BaseController
{
    public MimboxModelController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllMimboxModelsVm>> GetAllMimboxModels()
    {
        return Ok(await _mediator.Send(new GetAllMimboxModelsQuery { }));
    }

    [HttpGet("{id:guid}", Name = "MimboxModelById")]
    public async Task<ActionResult<MimboxModelByIdVm>> FilterMimboxModelsById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdMimboxModelQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateMimboxModel([FromBody] CreateMimboxModelRequestDto createMimboxModelRequestDto)
    {
        var createdMimboxModel = await _mediator.Send(new CreateMimboxModelCommand { CreateMimboxModelRequest = createMimboxModelRequestDto });

        return CreatedAtRoute("MimboxModelById", new { createdMimboxModel.Id }, createdMimboxModel);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMimboxModel([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteMimboxModelCommand { Id = id });

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateMimboxModel(Guid id, [FromBody] UpdateMimboxModelRequestDto updateMimboxModelRequestDto)
    {
        await _mediator.Send(new UpdateMimboxModelCommand { UpdateMimboxModelRequest = updateMimboxModelRequestDto, Id = id });

        return Ok("Mimbox model updated successfully");
    }
}