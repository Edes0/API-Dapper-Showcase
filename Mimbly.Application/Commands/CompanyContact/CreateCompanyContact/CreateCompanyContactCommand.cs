namespace Mimbly.Application.Commands.Company.CreateCompany;

using MediatR;
using Mimbly.Application.Contracts.Dtos.CompanyContact;

public class CreateCompanyContactCommand : IRequest
{
    public CreateCompanyContactRequestDto CreateCompanyContactRequest { get; set; } = null!;
}
