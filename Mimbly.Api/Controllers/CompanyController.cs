namespace Mimbly.Api.Controllers;

using FollowUp.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mimbly.Application.Commands.Company.CreateCompany;
using Mimbly.Application.Commands.Company.DeleteCompany;
using Mimbly.Application.Commands.Company.UpdateCompany;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Application.Queries.Company.GetAll;
using Mimbly.Application.Queries.Company.GetById;
using Mimbly.Application.Queries.Company.GetCompanyWithChildrenById;

[ApiController]
//[Authorize]
[Route("api/v1/[controller]")]
public class CompanyController : BaseController
{
    public CompanyController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<AllCompaniesVm>> GetAllCompanies()
    {
        return Ok(await _mediator.Send(new GetAllCompaniesQuery { }));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CompanyByIdVm>> FilterCompaniesById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetByIdCompanyQuery { Id = id }));
    }

    [Route("WithChildren/{id:guid}")]
    [HttpGet]
    public async Task<ActionResult<CompanyByIdVm>> CompanyWithChildrenById([BindRequired] Guid id)
    {
        return Ok(await _mediator.Send(new GetCompanyWithChildrenByIdQuery { Id = id }));
    }

    [HttpPost]
    public async Task<ActionResult> CreateCompany([FromBody] CreateCompanyRequestDto createCompanyRequestDto)
    {
        await _mediator.Send(new CreateCompanyContactCommand { CreateCompanyRequest = createCompanyRequestDto });

        return Ok("Company created successfully");
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCompany([BindRequired] Guid id)
    {
        await _mediator.Send(new DeleteCompanyCommand { Id = id });

        return Ok("Company removed successfully");
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyContactRequestDto updateCompanyRequestDto)
    {
        await _mediator.Send(new UpdateCompanyCommand { UpdateCompanyRequest = updateCompanyRequestDto, Id = id });

        return Ok("Company updated successfully");
    }
}