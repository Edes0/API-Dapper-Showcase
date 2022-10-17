namespace Mimbly.Application.Queries.Company.GetWithAllDataById;

using MediatR;

public record GetFilterWithChildrenByIdCompanyQuery : IRequest<CompanyWithChildrenFilteredById>
{
    public Guid Id { get; set; }
}