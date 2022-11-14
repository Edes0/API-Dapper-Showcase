namespace Mimbly.Application.Commands.CompanyContact.UpdateCompanyContact;

using MediatR;
using Mimbly.Application.Contracts.Dtos.CompanyContact;

public class UpdateCompanyContactCommand : IRequest
{
    public Guid Id { get; set; }
    public UpdateCompanyContactRequestDto UpdateCompanyContactRequest { get; set; } = null!;
}