namespace Mimbly.Application.Queries.Company.GetWithAllDataById;

using MediatR;

public record GetCompanyWithChildrenByIdQuery : IRequest<CompanyWithChildrenByIdVm>
{
    public Guid Id { get; set; }
}