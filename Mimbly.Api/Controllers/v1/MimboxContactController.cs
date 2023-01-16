namespace Mimbly.Api.Controllers.v1;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.MimboxContact.CreateMimboxContact;
using Mimbly.Application.Commands.MimboxContact.DeleteMimboxContact;
using Mimbly.Application.Commands.MimboxContact.UpdateMimboxContact;
using Mimbly.Application.Contracts.Dtos.MimboxContact;
using Mimbly.Application.Queries.MimboxContact.GetAll;
using Mimbly.Application.Queries.MimboxContact.GetById;

[ApiController]
//[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MimboxContactController : BaseController
{
    public MimboxContactController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllMimboxContactsVm>> GetAllMimboxContacts()
    {
        return Ok(await _mediator.Send(new GetAllMimboxContactsQuery { }));
    }

    [HttpGet("{id:guid}", Name = "MimboxContactById")]
    public async Task<ActionResult<MimboxContactByIdVm>> FilterCompaniesById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdMimboxContactQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateMimboxContact([FromBody] CreateMimboxContactRequestDto createMimboxContactRequestDto)
    {
        var createdMimboxContact = await _mediator.Send(new CreateMimboxContactCommand { CreateMimboxContactRequest = createMimboxContactRequestDto });

        return CreatedAtRoute("MimboxContactById", new { createdMimboxContact.Id }, createdMimboxContact);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteMimboxContact([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteMimboxContactCommand { Id = id });

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateMimboxContact(Guid id, [FromBody] UpdateMimboxContactRequestDto updateMimboxContactRequestDto)
    {
        await _mediator.Send(new UpdateMimboxContactCommand { UpdateMimboxContactRequest = updateMimboxContactRequestDto, Id = id });

        return Ok("Mimbox contact updated successfully");
    }
}