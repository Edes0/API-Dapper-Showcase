namespace Mimbly.Application.Commands.Company.CreateCompany;

using MediatR;
using Mimbly.Application.Contracts.Dtos.Company;
using Mimbly.Domain.Entities;

public class CreateCompanyCommand : IRequest<Company>
{
    public CreateCompanyRequestDto CreateCompanyRequest { get; set; } = null!;
}
