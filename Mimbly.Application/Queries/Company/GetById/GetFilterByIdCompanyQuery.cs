namespace Mimbly.Application.Queries.Company.GetById;

using MediatR;

public record GetFilterByIdCompanyQuery : IRequest<CompanyFilteredById>
{
    public Guid Id { get; set; }
}