namespace Mimbly.Application.Queries.Company.GetById;

using MediatR;

public record GetByIdCompanyQuery : IRequest<CompanyByIdVm>
{
    public Guid Id { get; set; }
}