namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.MimboxLogImage.DeleteMimboxLogImage;
using Mimbly.Application.Commands.MimboxLogImageImage.CreateMimboxLogImageImage;
using Mimbly.Application.Contracts.Dtos.MimboxLogImage;
using Mimbly.Application.Queries.MimboxLogImage.GetById;
using Mimbly.Application.Queries.MimboxLogImage.GetByMimboxLogId;

[ApiController]
//[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MimboxLogImageController : BaseController
{
    public MimboxLogImageController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("ByMimboxLogId/{mimboxLogId:guid}", Name = "MimboxLogImagesByMimboxLogId")]
    public async Task<ActionResult<MimboxLogImagesByMimboxLogIdVm>> FilterMimboxLogImagesByMimboxLogId([BindRequired] Guid mimboxLogId)
    {
        return Ok(await _mediator.Send(new GetByMimboxLogIdMimboxLogImageQuery { Id = mimboxLogId }));
    }

    [HttpGet("{id:guid}", Name = "MimboxLogImageById")]
    public async Task<ActionResult<MimboxLogImageByIdVm>> FilterMimboxLogImagesById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdMimboxLogImageQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateMimboxLogImage([FromBody] CreateMimboxLogImageRequestDto createMimboxLogImageRequestDto)
    {
        var createdMimboxLogImage = await _mediator.Send(new CreateMimboxLogImageCommand { CreateMimboxLogImageRequest = createMimboxLogImageRequestDto });

        return CreatedAtRoute("MimboxLogImageById", new { createdMimboxLogImage.Id }, createdMimboxLogImage);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMimboxLogImage([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteMimboxLogImageCommand { Id = id });

        return NoContent();
    }
}