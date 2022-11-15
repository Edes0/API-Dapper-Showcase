namespace Mimbly.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.CompanyContact.CreateCompanyContact;
using Mimbly.Application.Commands.CompanyContact.DeleteCompanyContact;
using Mimbly.Application.Commands.CompanyContact.UpdateCompanyContact;
using Mimbly.Application.Contracts.Dtos.CompanyContact;
using Mimbly.Application.Queries.CompanyContact.GetAll;
using Mimbly.Application.Queries.CompanyContact.GetById;
using v1;

[ApiController]
//[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CompanyContactController : BaseController
{
    public CompanyContactController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllCompanyContactsVm>> GetAllCompanyContacts()
    {
        return Ok(await _mediator.Send(new GetAllCompanyContactsQuery { }));
    }

    [HttpGet("{id:guid}", Name = "CompanyContactById")]
    public async Task<ActionResult<CompanyContactByIdVm>> FilterCompaniesById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdCompanyContactQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateCompanyContact([FromBody] CreateCompanyContactRequestDto createCompanyContactRequestDto)
    {
        var createdCompanyContact = await _mediator.Send(new CreateCompanyContactCommand { CreateCompanyContactRequest = createCompanyContactRequestDto });

        return CreatedAtRoute("CompanyContactById", new { createdCompanyContact.Id }, createdCompanyContact);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCompanyContact([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteCompanyContactCommand { Id = id });

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateCompanyContact(Guid id, [FromBody] UpdateCompanyContactRequestDto updateCompanyContactRequestDto)
    {
        await _mediator.Send(new UpdateCompanyContactCommand { UpdateCompanyContactRequest = updateCompanyContactRequestDto, Id = id });

        return Ok("Company contact updated successfully");
    }
}