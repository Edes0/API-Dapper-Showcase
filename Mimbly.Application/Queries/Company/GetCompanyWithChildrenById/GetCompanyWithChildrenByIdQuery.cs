namespace Mimbly.Application.Queries.Company.GetCompanyWithChildrenById;

using MediatR;

public record GetCompanyWithChildrenByIdQuery : IRequest<CompanyWithChildrenByIdVm>
{
    public Guid Id { get; set; }
}