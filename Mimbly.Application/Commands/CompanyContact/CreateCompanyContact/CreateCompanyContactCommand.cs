namespace Mimbly.Application.Commands.CompanyContact.CreateCompanyContact;

using MediatR;
using Mimbly.Application.Contracts.Dtos.CompanyContact;
using Mimbly.Domain.Entities;

public class CreateCompanyContactCommand : IRequest<CompanyContact>
{
    public CreateCompanyContactRequestDto CreateCompanyContactRequest { get; set; } = null!;
}
