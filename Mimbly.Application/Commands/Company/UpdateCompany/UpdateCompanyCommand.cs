namespace Mimbly.Application.Commands.Company.UpdateCompany;
 
using MediatR;
using Mimbly.Application.Contracts.Dtos.Company;

public class UpdateCompanyCommand : IRequest
{
    public Guid Id { get; set; }
    public UpdateCompanyContactRequestDto UpdateCompanyRequest { get; set; } = null!;
}
