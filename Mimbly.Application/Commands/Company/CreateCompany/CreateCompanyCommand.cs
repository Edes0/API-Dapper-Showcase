namespace Mimbly.Application.Commands.CompanyContact.CreateCompany;

using MediatR;
using Mimbly.Application.Contracts.Dtos.Company;

public class CreateCompanyCommand : IRequest
{
    public CreateCompanyRequestDto CreateCompanyRequest { get; set; } = null!;
}
